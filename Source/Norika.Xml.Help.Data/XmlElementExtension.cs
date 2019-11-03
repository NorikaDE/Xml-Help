using System.Xml;
using Norika.Xml.Help.Data.Interfaces;

namespace Norika.Xml.Help.Data
{
    /// <summary>
    /// Contains extension methods for comment based xml help.
    /// </summary>
    public static class XmlElementExtension
    {
        /// <summary>
        /// Extension method for getting the documentation based help for this
        /// xml element if any defined.
        /// </summary>
        /// <param name="element">Target element to get the help for.</param>
        /// <returns>Comment based xml help for the element.</returns>
        public static IXmlHelp GetHelp(this XmlElement element)
        {
            IXmlHelp keywords = new XmlHelp();
            if(element.PreviousSibling?.NodeType == XmlNodeType.Comment)
            {
                XmlHelpTextParser xmlHelpTextParser = new XmlHelpTextParser();
                keywords = xmlHelpTextParser.Parse(element.PreviousSibling?.InnerText);
            }
            return keywords;
        }
    }
}
