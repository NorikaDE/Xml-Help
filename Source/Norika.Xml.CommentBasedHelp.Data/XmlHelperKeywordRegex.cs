using System.Text.RegularExpressions;

namespace Norika.Xml.CommentBasedHelp.Data
{
    /// <summary>
    /// Helper struct for creating required RegRex for matching
    /// the xml comment based help
    /// </summary>
    internal struct XmlHelperKeywordRegex
    {
        /// <summary>
        /// The created regex object
        /// </summary>
        public Regex RegexObject;
        
        /// <summary>
        /// Name of the group that contains the keyword within the regex match
        /// </summary>
        public string KeywordGroupName;
        
        /// <summary>
        /// Name of the group that contains the additional information within the regex match
        /// </summary>
        public string AdditionalGroupName;
    }
}