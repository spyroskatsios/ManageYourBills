using MediatR;

namespace ManageYourBills.BlazorServer.Services;

public class Dispatcher : IDispatcher
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Dispatcher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public async Task<T> SendAsync<T>(IRequest<T> request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(request, cancellationToken);
    }
}