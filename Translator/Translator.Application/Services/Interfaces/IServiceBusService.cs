namespace Translator.Application.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendMessageAsync(Guid id);
    }
}
