using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fundraising.App.Core.Entities;
using Fundraising.App.Database;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Microsoft.AspNetCore.Authorization;

namespace Fundraising.App.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var allMembersResult = await _memberService.GetAllMembersAsync();
            return View(allMembersResult.Data);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService
                .GetMemberByIdAsync(id.Value);

            if (member.Error != null || member.Data ==  null)
            {
                return NotFound();
            }

            return View(member.Data);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,Email,Username,Password,Phone,Birthday,CreatedDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                await _memberService
                    .CreateMemberAsync(new OptionMember {
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        Address = member.Address,
                        Email = member.Email,
                        Username = member.Username,
                        Password = member.Password,
                        Phone = member.Phone,
                        Birthday = member.Birthday,
                        CreatedDate = DateTime.Now
                    });

                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService
                .GetMemberByIdAsync(id.Value);

            if (member.Error != null || member.Data == null)
            {
                return NotFound();
            }
            return View(member.Data);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,Email,Username,Password,Phone,Birthday,CreatedDate")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _memberService
                    .UpdateMemberByIdAsync(new OptionMember 
                    {
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        Address = member.Address,
                        Email = member.Email,
                        Username = member.Username,
                        Password = member.Password,
                        Phone = member.Phone,
                        Birthday = member.Birthday
                    }, id);


                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberByIdAsync(id.Value);
        
            if (member.Error != null || member.Data == null)
            {
                return NotFound();
            }

            return View(member.Data);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _memberService.DeleteMemberByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool MemberExists(int id)
        //{
        //    return _context.Members.Any(e => e.Id == id);
        //}
    }
}
