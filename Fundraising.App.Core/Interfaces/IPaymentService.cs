using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IPaymentService
    {
        public List<OptionPayment> GetAllPayments();


        Task<Result<OptionPayment>> CreatePaymentAsync(OptionPayment optionPayment, int Id);
        Task<Result<List<OptionPayment>>> GetAllPaymentsAsync();
        Task<Result<OptionPayment>> GetPaymentByIdAsync(int Id);
        Task<Result<int>> DeletePaymentByIdAsync(int Id);
    }
}
