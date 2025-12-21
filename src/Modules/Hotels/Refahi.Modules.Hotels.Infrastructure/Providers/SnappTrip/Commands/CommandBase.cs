
namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands;

public abstract class CommandBase<TRequest, TResponse>
{
    protected abstract string Endpoint { get; }

    public abstract Task<TResponse> Execute(HttpClient client, TRequest request);
}


public abstract class CommandBase<TRequest>
{
    protected abstract string Endpoint { get; }

    public abstract Task Execute(HttpClient client, TRequest request);
}