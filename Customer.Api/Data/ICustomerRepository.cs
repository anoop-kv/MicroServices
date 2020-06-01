using System.Threading.Tasks;

namespace Customer.Api.Data
{
    public interface ICustomerRepository
    {
        Task SaveAsync(CustomerDto customer);
    }
}