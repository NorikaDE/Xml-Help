using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.Xml.CommentBasedHelp.Data.Interfaces;

namespace Norika.Xml.CommentBasedHelp.Data.UnitTests
{
    [TestClass]
    public class XmlHelpElementExtensionUnitTest
    {
        [TestMethod]
        public void GetHelp_WithElementWithoutHelp_ShouldReturnListWithZeroEntries()
        {
            string elementStringWithoutHelp = "<element></element>";

            XmlElement element = CreateElement(elementStringWithoutHelp);

            IList<IXmlCommentHelpParagraph> help = element.GetHelp();
            
            Assert.AreEqual(0, help.Count);
        }
        
        [TestMethod]
        public void GetHelp_WithElementWithSynopsisAndDescription_ShouldReturnListWithTwoEntries()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<!--");
            stringBuilder.AppendLine(".SYNOPSIS");
            stringBuilder.AppendLine("Synopsis content");
            stringBuilder.AppendLine(".DESCRIPTION");
            stringBuilder.AppendLine("Description content");
            stringBuilder.AppendLine("-->");
            stringBuilder.AppendLine("<element></element>");
            
            XmlElement element = CreateElement(stringBuilder.ToString());

            IList<IXmlCommentHelpParagraph> help = element.GetHelp();
            
            Assert.AreEqual(2, help.Count);
        }

        private XmlElement CreateElement(string s)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(s);
            return document.DocumentElement;
        }
        
    }
}