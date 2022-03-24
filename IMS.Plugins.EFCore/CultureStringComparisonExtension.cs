namespace IMS.Plugins.EFCore
{
    internal static class CultureStringComparisonExtension
    {
        /// <summary>Performs a <see cref="StringComparison.CurrentCultureIgnoreCase"><c>CurrentCultureIgnoreCase</c></see> contains check.</summary>
        internal static bool IgnoreCaseContains(this string a, string b)
        {
            return a.Contains(b, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>Performs a <see cref="StringComparison.CurrentCultureIgnoreCase"><c>CurrentCultureIgnoreCase</c></see> equality check.</summary>
        internal static bool IgnoreCaseEquals(this string a, string b)
        {
            return a.Equals(b, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
