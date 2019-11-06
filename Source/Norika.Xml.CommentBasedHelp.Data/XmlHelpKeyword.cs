using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Norika.Xml.CommentBasedHelp.Data.Interfaces;

namespace Norika.Xml.CommentBasedHelp.Data
{
    /// <summary>
    /// Implementation of the <see cref="IXmlCommentHelpParagraph"/> interface providing
    /// information about the single paragraphs from the xml comment based help.
    /// </summary>
    public class XmlHelpKeyword : IXmlCommentHelpParagraph
    {
        /// <summary>
        /// Identifier prefix for new paragraphs. 
        /// </summary>
        private static readonly char HelpKeywordPrefixIdentifier = Options.HelpKeywordPrefixIdentifier;

        /// <summary>
        /// Constructor
        /// </summary>
        private XmlHelpKeyword()
        {
            Content = new List<string>();
        }
        
        /// <summary>
        /// <inheritdoc cref="IXmlCommentHelpParagraph.Name"/>
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// <inheritdoc cref="IXmlCommentHelpParagraph.Content"/>
        /// </summary>
        public IList<string> Content { get; }

        /// <summary>
        /// <inheritdoc cref="IXmlCommentHelpParagraph.Additional"/>
        /// </summary>
        public string Additional { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Header title of the paragraph</param>
        public XmlHelpKeyword(string name) : this()
        {
            Name = GetNameFromLine(name);
            Additional = GetAdditionalInformationFromLine(name);
        }

        /// <summary>
        /// Tries to resolve the name of the paragraph from
        /// a xml comment line by removing the prefix identifier and potential
        /// additional information.
        /// </summary>
        /// <param name="name">Header line of the xml comment based help paragraph</param>
        /// <returns>Name of the paragraph without prefix and additional information</returns>
        private static string GetNameFromLine(string name)
        {
            XmlHelperKeywordRegex keywordRegex = XmlHelpKeywordRegexBuilder.Create(HelpKeywordPrefixIdentifier);
            Match keywordRegexMatch = keywordRegex.RegexObject.Match(name);

            return keywordRegexMatch.Success ? keywordRegexMatch.Groups[keywordRegex.KeywordGroupName].Value : name;
        }
        
        /// <summary>
        /// Tries to resolve the additional content from 
        /// a xml comment based line by removing the prefix identifier and
        /// the name of the paragraph
        /// </summary>
        /// <param name="name">Header line of the xml comment based help paragraph</param>
        /// <returns>Additional information without prefix and header title</returns>
        private static string GetAdditionalInformationFromLine(string name)
        {
            XmlHelperKeywordRegex keywordRegex = XmlHelpKeywordRegexBuilder.Create(HelpKeywordPrefixIdentifier);
            Match keywordRegexMatch = keywordRegex.RegexObject.Match(name);

            return keywordRegexMatch.Groups[keywordRegex.AdditionalGroupName].Success ? 
                    keywordRegexMatch.Groups[keywordRegex.AdditionalGroupName].Value : String.Empty;
        }

        /// <summary>
        /// Adds a content line to the xml help paragraph
        /// </summary>
        /// <param name="line">Line to add.</param>
        internal void Add(string line)
        {
            Content.Add(line);
        }

        /// <summary>
        /// Checks if a given line is a xml comment based paragraph header title keyword. 
        /// </summary>
        /// <param name="text">Line to check</param>
        /// <returns>True if it is a keyword.</returns>
        public static bool IsKeyword(string text)
        {
            return XmlHelpKeywordRegexBuilder.Create(HelpKeywordPrefixIdentifier).RegexObject.IsMatch(text.Trim());
        }
    }
}