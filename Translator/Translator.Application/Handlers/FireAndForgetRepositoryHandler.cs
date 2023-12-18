using Microsoft.Extensions.DependencyInjection;
using Translator.Application.Handlers.Interfaces;
using Translator.Domain.Interfaces;

namespace Translator.Application.Handlers
{
    public class FireForgetRepositoryHandler(IServiceScopeFactory serviceScopeFactory) : IFireForgetRepositoryHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public void Execute(Func<ITranslationCommandRepository, Task> databaseWork)
        {
            Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<ITranslationCommandRepository>();
                    await databaseWork(repository);
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
