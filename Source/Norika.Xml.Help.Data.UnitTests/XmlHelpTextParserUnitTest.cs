using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.Xml.Help.Data.Interfaces;

namespace Norika.Xml.Help.Data.UnitTests
{
    [TestClass]
    public class XmlHelpTextParserUnitTest
    {
        private XmlHelpTextParser _sut;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
        }
        
        [TestInitialize]
        public void InitializeParser()
        {
            _sut = new XmlHelpTextParser();
        }

        [TestMethod]
        public void Parse_WithOneKeywordEntry_ShouldParseKeywordCorrectly()
        {
            const string testValueInput = ".SYNOPSIS\nShould return correct input.";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual("SYNOPSIS", returnValue[0].Name);        
        }
        
        [TestMethod]
        public void Parse_WithoutAnyKeywordEntryButComment_ShouldParseKeywordAsDescription()
        {
            const string testValueInput = "Should return correct input.";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual("DESCRIPTION", returnValue[0].Name);
        }
        
        [TestMethod]
        public void Parse_WithoutAnyKeywordEntryButComment_ShouldParseKeywordAsCorrectDescription()
        {
            const string testValueInput = "Should return correct input.";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual("DESCRIPTION", returnValue[0].Name);        
            Assert.AreEqual("Should return correct input.", returnValue[0].Content.First());        
        }

        [TestMethod]
        public void Parse_WithoutAnyKeywordEntryButMultiLineComment_ShouldParseKeywordAsDescriptionAndCorrectDescription()
        {
            const string testValueInput = "Should return correct input.";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual("DESCRIPTION", returnValue[0].Name);        
            Assert.AreEqual("Should return correct input.", returnValue[0].Content.First());        
        }
        
        [TestMethod]
        public void Parse_WithOneKeywordEntryAndAdditionalInformationSurroundedWithMsBuildFormat_ShouldParseKeywordCorrectly()
        {
            const string testValueInput = ".PARAMETER $(Property)\nShould return correct input.";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual("PARAMETER", returnValue[0].Name);        
            Assert.AreEqual("$(Property)", returnValue[0].Additional);        
            Assert.AreEqual("Should return correct input.", returnValue[0].Content.First());        
        }
        
        [TestMethod]
        public void Parse_WithOneKeywordEntryAndMultipleNewLineSeparator_ShouldParseKeywordCorrectly()
        {
            const string testValueInput = ".SYNOPSIS\n\rShould return correct input.";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual("SYNOPSIS", returnValue[0].Name);        
        }


        [TestMethod]
        public void Parse_WithOneKeywordEntryAndSingleLineContent_ShouldParseKeywordAndContentCorrectly()
        {
            const string testValueInput = ".SYNOPSIS\nShould return correct input.\nWith stupid input";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual("SYNOPSIS", returnValue[0].Name);
        }

        [TestMethod]
        public void Parse_WithOneKeywordEntryAndMultipleLineContent_ShouldParseKeywordAndFirstLineCorrectly()
        {
            const string keywordValue = "SYNOPSIS";
            const string keywordContent = "Should return correct input.";
            const string secondLineContent = "bla";

            string testValueInput = $".{keywordValue}\n{keywordContent}\n{secondLineContent}";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual(keywordValue, returnValue[0].Name);
            Assert.AreEqual(keywordContent, returnValue[0].Content[0]);
        }
        
        [TestMethod]
        public void Parse_WithOneKeywordEntryAndMultipleLineContentAsList_ShouldParseKeywordAndFirstLineCorrectly()
        {
            IList<string> inputValue = new List<string>()
            {
                ".SYNOPSIS",
                "Should return correct input",
                "bla"
            };

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(inputValue);

            Assert.AreEqual("SYNOPSIS", returnValue[0].Name);
            Assert.AreEqual("Should return correct input", returnValue[0].Content[0]);
            Assert.AreEqual("bla", returnValue[0].Content[1]);
        }

        [TestMethod]
        public void Parse_WithOneKeywordEntryAndMultipleLineContent_ShouldParseKeywordAndContentCorrectly()
        {
            const string keywordValue = "SYNOPSIS";
            const string keywordContent = "Should return correct input.";
            const string keywordSecondLineContent = "Should return correct second line input.";

            string testValueInput =
                $".{keywordValue}\n{keywordContent}\n{keywordSecondLineContent}";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual(keywordValue, returnValue[0].Name);
            Assert.AreEqual(keywordContent, returnValue[0].Content[0]);
            Assert.AreEqual(keywordSecondLineContent, returnValue[0].Content[1]);
        }

        [TestMethod]
        public void Parse_WithTwoKeywordsWithoutValue_ShouldReturnTwoKeywordsWithoutValues()
        {
            const string keyword1 = "RADIO";
            const string keyword2 = "GAGA";

            string testValue = $".{keyword1}\n.{keyword2}";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValue);

            Assert.AreEqual(keyword1, returnValue[0].Name);
            Assert.AreEqual(keyword2, returnValue[1].Name);
        }

        [TestMethod]
        public void Parse_WithTwoKeywordsAndOneLineContent_ShouldParseCorrectKeywords()
        {
            const string keywordValue = "SYNOPSIS";
            const string keywordContent = "Should return correct synopsis input.";

            const string keyword2Value = "DESCRIPTION";
            const string keyword2Content = "Should return correct description input.";

            string testValueInput =
                $".{keywordValue}\n{keywordContent}\n.{keyword2Value}\n{keyword2Content}";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);


            Assert.AreEqual(keywordContent, returnValue.FirstOrDefault(
                s => s.Name.Equals(keywordValue))?.Content[0]);
            Assert.AreEqual(keyword2Content, returnValue.FirstOrDefault(
                s => s.Name.Equals(keyword2Value))?.Content[0]);
        }
        
        [TestMethod]
        public void Parse_WithKeywordAndAdditionalInformation_ShouldParseCorrectKeywordAndAdditionalInformation()
        {
            const string keywordValue = "PARAMETER";
            const string keywordValueAdditional = "InputName";
            const string keywordContent = "Should return correct parameter input and name.";

            string testValueInput =
                $".{keywordValue} {keywordValueAdditional}\n{keywordContent}";

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testValueInput);

            Assert.AreEqual(keywordValue, returnValue[0].Name);
            Assert.AreEqual(keywordValueAdditional, returnValue[0].Additional);
            Assert.AreEqual(keywordContent, returnValue[0].Content[0]);
        }
        
        [TestMethod]
        [DeploymentItem(@"TestData/TestXmlHelpFile.txt")]
        public void Parse_FromTestFile_ShouldParseAllKeywordsCorrectly()
        {
            string testContent = File.ReadAllText("TestData/TestXmlHelpFile.txt");

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testContent);

            Assert.AreEqual(4, returnValue.Count);
        }
        
        [TestMethod]
        [DeploymentItem(@"TestData/TestXmlHelpFile.txt")]
        public void Parse_FromTestFile_ShouldParseAllKeywordsWithCorrectName()
        {
            string testContent = File.ReadAllText("TestData/TestXmlHelpFile.txt");

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testContent);

            Assert.AreEqual("DESCRIPTION", returnValue[0].Name);
            Assert.AreEqual("SYNOPSIS", returnValue[1].Name);
            Assert.AreEqual("PARAMETER", returnValue[2].Name);
            Assert.AreEqual("PARAMETER", returnValue[3].Name);
        }
        
        [TestMethod]
        [DeploymentItem(@"TestData/TestXmlHelpFile.txt")]
        public void Parse_FromTestFile_ShouldParseAllKeywordsWithCorrectContent()
        {
            string testContent = File.ReadAllText("TestData/TestXmlHelpFile.txt");

            IList<IXmlCommentHelpParagraph> returnValue = _sut.Parse(testContent);

            Assert.AreEqual("This is a test description", returnValue[0].Content.First());
            Assert.AreEqual("This is a test synopsis", returnValue[1].Content.First());
            Assert.AreEqual("This is the test input parameter description", returnValue[2].Content.First());
            Assert.AreEqual("Input", returnValue[2].Additional);
            Assert.AreEqual("This is the test output parameter description", returnValue[3].Content.First());
            Assert.AreEqual("Output", returnValue[3].Additional);
        }

    }
}
