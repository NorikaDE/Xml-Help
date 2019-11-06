using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Norika.Xml.CommentBasedHelp.Data.UnitTests
{
    [TestClass]
    public class XmlHelpKeywordUnitTest
    {
       

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestValueWithoutLeadingDot_ShouldReturnFalse()
        {
            Assert.IsFalse(XmlHelpKeyword.IsKeyword("Test"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestValueAndLeadingSlash_ShouldReturnFalse()
        {
            Assert.IsFalse(XmlHelpKeyword.IsKeyword("/Test"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestValueAndLeadingBacklash_ShouldReturnFalse()
        {
            Assert.IsFalse(XmlHelpKeyword.IsKeyword("/Test"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestValueAndLeadingMinus_ShouldReturnFalse()
        {
            Assert.IsFalse(XmlHelpKeyword.IsKeyword("-Test"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestValueLeadingDot_ShouldReturnTrue()
        {
            Assert.IsTrue(XmlHelpKeyword.IsKeyword(".Test"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestValueLeadingDotAndWhiteSpacesBefore_ShouldIgnoreLeadingWhiteSpacesAndReturnTrue()
        {
            Assert.IsTrue(XmlHelpKeyword.IsKeyword("    .Test"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestAndFollowingInformation_ShouldIgnoreFollowingInformationAndReturnTrue()
        {
            Assert.IsTrue(XmlHelpKeyword.IsKeyword(".Test MyTest"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestAndClosingWhitespace_ShouldIgnoreClosingWhitespaceAndReturnTrue()
        {
            Assert.IsTrue(XmlHelpKeyword.IsKeyword(".Test    "));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestAndLeadingAndClosingWhitespace_ShouldIgnoreWhitespacesAtBothEndsAndReturnTrue()
        {
            Assert.IsTrue(XmlHelpKeyword.IsKeyword("      .Test    "));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestLowerCase_ShouldIgnoreCaseAndReturnTrue()
        {
            Assert.IsTrue(XmlHelpKeyword.IsKeyword(".test"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestAllUpperCase_ShouldIgnoreCaseAndReturnTrue()
        {
            Assert.IsTrue(XmlHelpKeyword.IsKeyword(".TEST"));
        }

        [TestMethod]
        public void IsKeyword_WithStringLiteralTestAllUpperCaseAndLeadingWhitespaces_ShouldIgnoreCaseAndWhitespacesAndReturnTrue()
        {
            Assert.IsTrue(XmlHelpKeyword.IsKeyword("    .TEST    "));
        }
    }
}
