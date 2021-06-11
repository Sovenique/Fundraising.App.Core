using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Web.Services;
using Fundraising.App.Core.Options;

namespace Fundraising.App.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly IProjectService _projectService;
        private readonly IRewardService _rewardService; 
        private readonly IPaymentService _paymentService;
        private readonly IMemberService _memberService;
        private readonly ICurrentUserService _currentUserService;

        public APIController(
            IApplicationDbContext context,
            ICurrentUserService currentUserService,
            IProjectService projectService,
            IRewardService rewardService,
            IPaymentService paymentService,
            IMemberService memberService
            )
        {
            _context = context;
            _currentUserService = currentUserService;
            _projectService = projectService;
            _rewardService = rewardService;
            _paymentService = paymentService;
            _memberService = memberService;
        }

        // GET: /api/API/GetProjects
        [HttpGet("GetProjects")]
        public async Task<ActionResult<IEnumerable<OptionsProject>>> GetProjects()
        {
            var AllProjects = await _projectService.GetAllProjectsAsync();
            return AllProjects.Data;
        }

        // GET: /api/API/GetProjectByCreatorId
        [HttpGet("GetProjectByCreatorId")]
        public async Task<ActionResult<List<OptionsProject>>> GetProjectByCreatorId()
        {
            string creatorId = _currentUserService.UserId;
            var AllProjects = await _projectService.GetProjectByCreatorIdAsync(creatorId);
            return AllProjects.Data;
        }

        // GET: /api/API/GetPaymentsOfProjects
        [HttpGet("GetPaymentsOfProjects")]
        public ActionResult<IEnumerable<ProjectPayments>> GetPaymentsOfProjects()
        {
            List<ProjectPayments> projectPayments = new();

            var members = _memberService.GetAllMembers();
            var projects = _projectService.GetAllProjects();
            var rewards = _rewardService.GetAllRewards();
            projects.ForEach(project => {

                var payments = _paymentService.GetAllPaymentsByProjectId(project.Id);
                payments.ForEach(payment => {
                    var reward = rewards.Where(re => re.Id == payment.RewardId).ToList();
                    var member = members.Where(mem => mem.Id == payment.MemberId).ToList();

                    
                    if (member.Count != 0)
                    {
                        // Debug.WriteLine($"Member:{member[0].Email} : {member[0].Id} , project:{project.Title} , reward:{reward[0].Title} ");
                        projectPayments.Add(new ProjectPayments
                        {
                            ProjectName = project.Title,
                            MemberEmail = member[0].Email,
                            Value = reward[0].Value,
                            Date = payment.PaymentDate.Date.ToShortDateString() + "  -  " + payment.PaymentDate.Hour +":"+ payment.PaymentDate.Minute
                        });
                    }
                });
            });


            return projectPayments.ToArray();
        }

        public class ProjectPayments
        {
            public string ProjectName { get; set; }
            public string MemberEmail { get; set; }
            public decimal Value { get; set; }
            public string Date { get; set; }

        }


        // GET: api/ProjectsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/ProjectsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Projects.Add(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProjectsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/ProjectsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
