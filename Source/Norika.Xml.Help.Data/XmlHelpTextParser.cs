using System;
using System.Collections.Generic;
using Norika.Xml.Help.Data.Interfaces;

namespace Norika.Xml.Help.Data
{
    /// <summary>
    /// Parser for text based help.
    /// </summary>
    public class XmlHelpTextParser
    {
        /// <summary>
        /// Parses the given text and returns an object implementing
        /// the <see cref="IXmlHelp"/> interface representing the text
        /// based help.
        /// </summary>
        /// <param name="helpText">Help text to parse</param>
        /// <returns>Parsed help</returns>
        public IXmlHelp Parse(string helpText)
        {
            return Parse(PrepareInput(helpText));
        }
        
        /// <summary>
        /// Parses the given text and returns an object implementing
        /// the <see cref="IXmlHelp"/> interface representing the text
        /// based help.
        /// </summary>
        /// <param name="help">Help text lines to parse</param>
        /// <returns>Parsed help</returns>
        public IXmlHelp Parse(IList<string> help)
        {
            IXmlHelp keywords = DoParseXmlHelp(help);

            if (keywords.Count == 0)
            {
                ApplyFallBackStrategy(string.Join('\n', help), keywords);
            }
            return keywords;
        }

        /// <summary>
        /// Main implementation of the help parser
        /// </summary>
        /// <param name="helpContent">Help text to parse</param>
        /// <returns>Parsed help</returns>
        private IXmlHelp DoParseXmlHelp(IEnumerable<string> helpContent)
        {
            IXmlHelp keywords = new XmlHelp();
            XmlHelpKeyword currentXmlHelpKeyword = null;

            foreach(string lineValue in helpContent)
            {
                string trimmedLine = lineValue.Trim();
                if (XmlHelpKeyword.IsKeyword(trimmedLine))
                {
                    currentXmlHelpKeyword = new XmlHelpKeyword(trimmedLine);
                    keywords.Add(currentXmlHelpKeyword);
                }else
                {
                    currentXmlHelpKeyword?.Add(trimmedLine);
                }
            }
            return keywords;
        }

        
        /// <summary>
        /// Applies a fall back strategy by using the given text as the default
        /// help paragraph section. 
        /// </summary>
        /// <param name="helpText">The help text to apply the fallback on</param>
        /// <param name="keywords">Parsed help</param>
        private void ApplyFallBackStrategy(string helpText, IXmlHelp keywords)
        {
            IXmlCommentHelpParagraph fallBackParagraph = GetFallbackDescription(helpText);
            if (fallBackParagraph != null)
            {
                keywords.Add(fallBackParagraph);
            }
        }

        
        /// <summary>
        /// Prepares the input for further process
        /// </summary>
        /// <param name="helpText">Help text to prepare</param>
        /// <returns>Prepared input</returns>
        private IList<string> PrepareInput(string helpText)
        {
            IList<string> splitByNewLine = helpText.Split('\n');
            return splitByNewLine;
        }

        
        /// <summary>
        /// Applies the default help keyword to the given help text.
        /// </summary>
        /// <param name="helpText">Help text to apply the default keyword to.</param>
        /// <returns>Processed help</returns>
        private IXmlCommentHelpParagraph GetFallbackDescription(string helpText)
        {
            if (string.IsNullOrWhiteSpace(helpText)) return null;
            
            IList<string> slittedByNewLine = helpText.Split('\n');
            XmlHelpKeyword xmlHelp = new XmlHelpKeyword(Options.DefaultXmlHelpKeyword);
            foreach (string line in slittedByNewLine)
            {
                xmlHelp.Add(line);
            }

            return xmlHelp;
        }
    }

    
}
