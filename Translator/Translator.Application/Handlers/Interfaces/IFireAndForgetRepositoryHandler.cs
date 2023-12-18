using Translator.Domain.Interfaces;

namespace Translator.Application.Handlers.Interfaces
{
    public interface IFireForgetRepositoryHandler
    {
        void Execute(Func<ITranslationCommandRepository, Task> databaseWork);
    }
}