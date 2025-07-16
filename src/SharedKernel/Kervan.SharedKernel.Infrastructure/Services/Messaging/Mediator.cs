using Kervan.SharedKernel.Application.Messaging;

namespace Kervan.SharedKernel.Infrastructure.Services.Messaging;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        // Fabrikadan bu isteğe özel, önceden derlenmiş çalıştırıcıyı al.
        var runner = RequestHandlerFactory.GetRunner(request.GetType());

        // Çalıştırıcıyı, o anki isteğe özel serviceProvider ile çalıştır.
        var result = await (Task<TResponse>)runner(request, _serviceProvider, cancellationToken);

        return result;
    }

    public Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}