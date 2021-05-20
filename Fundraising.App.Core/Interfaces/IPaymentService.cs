using Fundraising.App.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IPaymentService
    {
        public OptionPayment CreatePayment(OptionPayment optionPayment);
        public List<OptionPayment> ReadAllPayments();
        public OptionPayment UpdatePayment(OptionPayment optionPayment, int Id);
        public bool DeletePayment(int Id);
    }
}
