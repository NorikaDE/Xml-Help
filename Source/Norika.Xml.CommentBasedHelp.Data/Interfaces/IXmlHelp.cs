using System;
using System.Collections.Generic;

namespace Norika.Xml.CommentBasedHelp.Data.Interfaces
{
    /// <summary>
    /// Collection of <see cref="IXmlCommentHelpParagraph"/>
    /// </summary>
    public interface IXmlHelp : IList<IXmlCommentHelpParagraph>
    {
        /// <summary>
        /// Returns all help paragraphs containing the given paragraph name for a specified xml element.
        /// </summary>
        /// <param name="paragraphName">String identifies the paragraphs to look up for</param>
        /// <returns>Collection of <see cref="IXmlCommentHelpParagraph"/> specified for the current xml element.</returns>
        IList<IXmlCommentHelpParagraph> LookUp(string paragraphName);
            
        /// <summary>
        /// Returns all help paragraphs containing the given paragraph name for a specified xml element.
        /// Considering entries based on the given string comparision. 
        /// </summary>
        /// <param name="paragraphName">String identifies the paragraphs to look up for</param>
        /// <param name="comparison">String comparision to consider for the paragraph lookup</param>
        /// <returns>Collection of <see cref="IXmlCommentHelpParagraph"/> specified for the current xml element.</returns>
        IList<IXmlCommentHelpParagraph> LookUp(string paragraphName, StringComparison comparison);

        /// <summary>
        /// Removes the the paragraphs matching the given name.
        /// </summary>
        /// <param name="paragraphName">Paragraph that need to be removed.</param>
        /// <param name="distinctOnly">Indicates whether all matching paragraphs should be removed
        /// or only a unique, distinct one.</param>
        /// <returns></returns>
        bool Remove(string paragraphName, bool distinctOnly);
    }
}