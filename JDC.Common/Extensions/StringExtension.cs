using NickBuhro.Translit;

namespace JDC.Common.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// An string extension method that convert text to latin.
        /// </summary>
        /// <param name="text">The value to act on.</param>
        /// <returns>The latin text.</returns>
        public static string ToLatin(this string text)
        {
            return Transliteration.CyrillicToLatin(text, Language.Russian);
        }
    }
}
