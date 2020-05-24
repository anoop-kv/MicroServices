using System.Threading;
using System.Threading.Tasks;

namespace Common.Infrastructure.Contract
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}