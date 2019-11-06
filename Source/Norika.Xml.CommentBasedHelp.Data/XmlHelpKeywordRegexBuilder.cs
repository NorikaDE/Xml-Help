using System.Text.RegularExpressions;

namespace Norika.Xml.CommentBasedHelp.Data
{
    /// <summary>
    /// Builder for creating regular expression to identify xml based paragraph header keywords
    /// </summary>
    internal static class XmlHelpKeywordRegexBuilder
    {
        /// <summary>
        /// Name of the regex group containing the keyword for the paragraph header
        /// </summary>
        private static readonly string _helpKeywordRegexGroupName = "Keyword";
        /// <summary>
        /// Name of the regex group containing additional information about the paragraph header
        /// </summary>
        private static readonly string _helpKeywordAdditionalRegexGroupName = "Additional";
        /// <summary>
        /// Regex pattern for resolving the keyword and additonal information
        /// </summary>
        private static readonly string _helpKeywordRegexPattern = 
            "\\<PrefixIdentifier>(?'Keyword'[A-Za-z1-9]+)(\\s+(?'Additional'[A-Za-zäöüÄÖÜ1-9\\-_\\.\\(\\)\\@\\$]+)){0,1}$";

        /// <summary>
        /// Creates a regular expression for extracting paragraph header keywords and
        /// additional information from a line of the comment based help.
        /// </summary>
        /// <param name="prefixKeywordIdentifier">Identifier for new help keyword section</param>
        /// <returns>The built regex wrapped with meta information.</returns>
        internal static XmlHelperKeywordRegex Create(char prefixKeywordIdentifier)
        {
            return new XmlHelperKeywordRegex
            {
                RegexObject =
                    new Regex(
                        _helpKeywordRegexPattern.Replace("<PrefixIdentifier>", 
                            prefixKeywordIdentifier.ToString()),
                        RegexOptions.IgnoreCase),
                KeywordGroupName = _helpKeywordRegexGroupName,
                AdditionalGroupName = _helpKeywordAdditionalRegexGroupName
            };
        }
    }
}