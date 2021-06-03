using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<Result<Payment>> CreatePaymentAsync(OptionPayment optionPayment, int Id);
        Task<Result<List<Payment>>> GetAllPaymentsAsync();
        Task<Result<Payment>> GetPaymentByIdAsync(int Id);
        Task<Result<int>> DeletePaymentByIdAsync(int Id);
    }
}
