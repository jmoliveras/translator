namespace Translator.Infrastructure.Data.SqlQueries
{
    public static class TranslationQueries
    {
        public static string GetTranslationByIdQuery() =>
            "SELECT * FROM Translations WHERE Id = @id";
    }
}
