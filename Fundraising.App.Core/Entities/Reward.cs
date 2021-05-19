using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Entities
{
    public class Reward
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public int ProjectId { get; set; }
        public List<Payment> Payments { set; get; }
    }
}
