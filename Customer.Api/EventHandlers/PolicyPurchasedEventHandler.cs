using System.Threading;
using System.Threading.Tasks;
using Common.Infrastructure.Contract;
using Newtonsoft.Json.Linq;

namespace Customer.Api.EventHandlers
{
    public class PolicyPurchasedEventHandler : IServiceEventHandler
    {
        public Task HandleAsync(JObject jObject, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}