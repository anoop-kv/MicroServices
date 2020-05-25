using System;
using System.Threading;
using System.Threading.Tasks;

namespace Policy.Api.Data
{
    public interface IPolicyRepository 
    {
        Task<Policy> GetByIdAsync(Guid id);

        Task SaveAsync(Policy policy);
    }
}