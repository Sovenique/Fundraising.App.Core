using Fundraising.App.Core.Options;
using System.Collections.Generic;


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
