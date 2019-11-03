using System.Collections.Generic;

namespace Norika.Xml.Help.Data.Interfaces
{
    public interface IXmlCommentHelpParagraph
    {
        /// <summary>
        /// The header title of the xml help paragraph. 
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Contains all lines implemented under the header title. 
        /// </summary>
        IList<string> Content { get; }
        
        /// <summary>
        /// Additional information about the header of this paragraph
        /// </summary>
        string Additional { get; }
    }
}