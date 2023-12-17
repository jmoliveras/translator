namespace Translator.Infrastructure.Data.SqlQueries
{
    public static class TranslationQueries
    {
        public static string GetTranslationByIdQuery() =>
            "SELECT Text FROM Translations WHERE Id = @id";
    }
}
