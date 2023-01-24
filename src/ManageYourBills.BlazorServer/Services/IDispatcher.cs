using MediatR;

namespace ManageYourBills.BlazorServer.Services;

public interface IDispatcher
{
    Task<T> SendAsync<T>(IRequest<T> request, CancellationToken cancellationToken = default);
}