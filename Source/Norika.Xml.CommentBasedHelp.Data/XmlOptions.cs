namespace Norika.Xml.CommentBasedHelp.Data
{
    public class XmlHelpOptions
    {
        public static readonly XmlHelpOptions Default = new XmlHelpDefaultOptions();

        private XmlHelpOptions() { }
        
        private class XmlHelpDefaultOptions : XmlHelpOptions
        {
            internal XmlHelpDefaultOptions()
            {
                HelpKeywordPrefixIdentifier = '.';
                DefaultXmlHelpKeyword = "DESCRIPTION";
            }
        }
        
        /// <summary>
        /// Identifier prefix a help title keyword is starting with
        /// </summary>
        public char HelpKeywordPrefixIdentifier { get; private set; }
        
        /// <summary>
        /// Default help section keyword
        /// </summary>
        public string DefaultXmlHelpKeyword { get; private set; }
    }
}