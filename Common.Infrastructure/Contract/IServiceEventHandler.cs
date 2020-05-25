using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Common.Infrastructure.Contract
{
    public interface IServiceEventHandler
    {
        Task HandleAsync(JObject jObject, CancellationToken cancellationToken);
    }
}