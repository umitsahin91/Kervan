namespace Kervan.SharedKernel.Application.Messaging;
public interface ICommand<TResponse> : IRequest<TResponse> { }
public interface ICommand : IRequest { } // Cevap döndürmeyen komutlar için