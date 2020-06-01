using System;
using System.Threading;
using System.Threading.Tasks;

namespace Policy.Api.Data
{
    public interface IPolicyRepository 
    {
        Task<PolicyDto> GetByIdAsync(string id);

        Task SaveAsync(PolicyDto policy);
    }
}