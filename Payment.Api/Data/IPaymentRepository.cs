using System.Threading.Tasks;

namespace Payment.Api.Data
{
    public interface IPaymentRepository 
    {
        Task SaveAsync(BankAccountDto bankAccount);
    }
}