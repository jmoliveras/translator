namespace Translator.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool ExceedsLength(this string str, int length) => str.Length > length;        
    }
}
