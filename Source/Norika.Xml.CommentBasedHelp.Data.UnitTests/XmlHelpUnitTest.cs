using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.Xml.CommentBasedHelp.Data.Interfaces;

namespace Norika.Xml.CommentBasedHelp.Data.UnitTests
{
    [TestClass]
    public class XmlHelpUnitTest
    {
        [TestMethod]
        public void Clear_OnWrapperObject_ShouldCallClearOnTheBaseObject()
        {
            Mock<IList<IXmlCommentHelpParagraph>> listMock = new Mock<IList<IXmlCommentHelpParagraph>>();

            XmlHelp sutHelpObject = new XmlHelp(listMock.Object);
            sutHelpObject.Clear();
            
            listMock.Verify(l => l.Clear(), Times.Exactly(1));
        }

        [TestMethod]
        public void RemoveAt_OnWrapperObject_ShouldCallRemoveAtOnTheBaseObject()
        {
            Mock<IList<IXmlCommentHelpParagraph>> listMock = new Mock<IList<IXmlCommentHelpParagraph>>();

            XmlHelp sutHelpObject = new XmlHelp(listMock.Object);
            sutHelpObject.RemoveAt(99);

            listMock.Verify(l => l.RemoveAt(
                    It.Is((int i) => i == 99)),
                Times.Exactly(1));
        }
        
        [TestMethod]
        public void IndexOf_OnWrapperObject_ShouldCallIndexOfOnTheBaseObject()
        {
            string testParagraphName = "TestParagraph";
            
            Mock<IList<IXmlCommentHelpParagraph>> listMock = new Mock<IList<IXmlCommentHelpParagraph>>();
            Mock<IXmlCommentHelpParagraph> paragraphMock = new Mock<IXmlCommentHelpParagraph>();
            paragraphMock.Setup(pm => pm.Name).Returns(testParagraphName);
            listMock.Setup(lm => lm.IndexOf(It.Is<IXmlCommentHelpParagraph>(
                (paragraph => paragraph.Name.Equals(testParagraphName)))))
                .Returns(21);
            
            XmlHelp sutHelpObject = new XmlHelp(listMock.Object);
            int i = sutHelpObject.IndexOf(paragraphMock.Object);

            Assert.AreEqual(21, i);
        }
        
        [TestMethod]
        public void Remove_OnWrapperObjectWithMockInput_ShouldCallRemoveWithTheGivenObjectOnTheBaseObject()
        {
            string testParagraphName = "TestParagraph";
            
            Mock<IList<IXmlCommentHelpParagraph>> listMock = new Mock<IList<IXmlCommentHelpParagraph>>();
            Mock<IXmlCommentHelpParagraph> paragraphMock = new Mock<IXmlCommentHelpParagraph>();
            paragraphMock.Setup(pm => pm.Name).Returns(testParagraphName);
            listMock.Setup(lm => lm.Remove(It.Is<IXmlCommentHelpParagraph>(
                    (paragraph => paragraph.Name.Equals(testParagraphName)))))
                .Returns(true);
            
            XmlHelp sutHelpObject = new XmlHelp(listMock.Object);
            bool removed = sutHelpObject.Remove(paragraphMock.Object);

            Assert.IsTrue(removed);
            listMock.Verify(lm => lm.Remove(
                It.Is<IXmlCommentHelpParagraph>(paragraph => paragraph.Name.Equals(testParagraphName)))
                ,Times.Exactly(1));
        }
        
        [TestMethod]
        public void Contains_OnWrapperObjectWithMockInput_ShouldCallContainsWithTheGivenObjectOnTheBaseObject()
        {
            string testParagraphName = "TestParagraph";
            
            Mock<IList<IXmlCommentHelpParagraph>> listMock = new Mock<IList<IXmlCommentHelpParagraph>>();
            Mock<IXmlCommentHelpParagraph> paragraphMock = new Mock<IXmlCommentHelpParagraph>();
            paragraphMock.Setup(pm => pm.Name).Returns(testParagraphName);
            listMock.Setup(lm => lm.Contains(It.Is<IXmlCommentHelpParagraph>(
                    (paragraph => paragraph.Name.Equals(testParagraphName)))))
                .Returns(true);
            
            XmlHelp sutHelpObject = new XmlHelp(listMock.Object);
            bool contains = sutHelpObject.Contains(paragraphMock.Object);

            Assert.IsTrue(contains);
            listMock.Verify(lm => lm.Contains(
                    It.Is<IXmlCommentHelpParagraph>(paragraph => paragraph.Name.Equals(testParagraphName)))
                ,Times.Exactly(1));
        }
        
        [TestMethod]
        public void Insert_OnWrapperObjectWithMockInput_ShouldCallInsertWithTheGivenObjectOnTheBaseObject()
        {
            string testParagraphName = "TestParagraph";
            
            Mock<IList<IXmlCommentHelpParagraph>> listMock = new Mock<IList<IXmlCommentHelpParagraph>>();
            Mock<IXmlCommentHelpParagraph> paragraphMock = new Mock<IXmlCommentHelpParagraph>();
            paragraphMock.Setup(pm => pm.Name).Returns(testParagraphName);
            listMock.Setup(lm => lm.Insert(It.IsAny<int>(), It.Is<IXmlCommentHelpParagraph>(
                    (paragraph => paragraph.Name.Equals(testParagraphName)))));
            
            XmlHelp sutHelpObject = new XmlHelp(listMock.Object);
            sutHelpObject.Insert(1, paragraphMock.Object);
            
            listMock.Verify(lm => lm.Insert(It.Is((int i) => i == 1),
                    It.Is<IXmlCommentHelpParagraph>(paragraph => paragraph.Name.Equals(testParagraphName)))
                ,Times.Exactly(1));
        }
        
        [TestMethod]
        public void LookUp_WithOneMatchingContentInCollection_ShouldReturnMatchingItem()
        {
            string entryNamePattern = "TestParagraph";

            IList<IXmlCommentHelpParagraph> list = CreateListWithMockContent(entryNamePattern, 10);
            
            XmlHelp sutHelpObject = new XmlHelp(list);
            IList<IXmlCommentHelpParagraph> foundObjects = 
                sutHelpObject.LookUp($"{entryNamePattern}1");
            
            Assert.AreEqual(1, foundObjects.Count);
            Assert.AreEqual($"{entryNamePattern}1", foundObjects.First().Name);
        }
        
        
        [TestMethod]
        public void Remove_WithStringValueRepresentingExistingItem_ShouldRemoveItem()
        {
            string entryNamePattern = "TestParagraph";

            IList<IXmlCommentHelpParagraph> list = CreateListWithMockContent(entryNamePattern, 10);
            
            XmlHelp sutHelpObject = new XmlHelp(list);
            bool removedItem = 
                sutHelpObject.Remove($"{entryNamePattern}0", false);
            
            Assert.IsTrue(removedItem);
            Assert.AreEqual(9, sutHelpObject.Count);
            Assert.AreEqual(0, sutHelpObject.LookUp($"{entryNamePattern}0").Count);
        }
        
        
        [TestMethod]
        public void Iterate_OverAllItems_ShouldGetEveryItem()
        {
            string entryNamePattern = "TestParagraph";

            IList<IXmlCommentHelpParagraph> list = CreateListWithMockContent(entryNamePattern, 10);
            
            XmlHelp sutHelpObject = new XmlHelp(list);

            int counter = 0;
            
            foreach (IXmlCommentHelpParagraph paragraph in sutHelpObject.OrderBy(p => p.Name))
            {
                Assert.AreEqual($"{entryNamePattern}{counter}", paragraph.Name);
                counter++;
            }
            
            Assert.AreEqual(10, counter);
        }
        
        
        [TestMethod]
        public void LookUp_WithOneMatchingContentButDifferentCaseNameInCollection_ShouldReturnEmptyCollection()
        {
            string entryNamePattern = "TestParagraph";

            IList<IXmlCommentHelpParagraph> list = CreateListWithMockContent(entryNamePattern, 10);
            
            XmlHelp sutHelpObject = new XmlHelp(list);
            IList<IXmlCommentHelpParagraph> foundObjects = 
                sutHelpObject.LookUp($"TESTPARAGRAPH1");
            
            Assert.AreEqual(0, foundObjects.Count);
        }
        
        [TestMethod]
        public void LookUp_WithOneMatchingContentAndStringComparisonIgnoreCaseInCollection_ShouldReturnMatchingItem()
        {
            string entryNamePattern = "TestParagraph";

            IList<IXmlCommentHelpParagraph> list = CreateListWithMockContent(entryNamePattern, 10);
            
            XmlHelp sutHelpObject = new XmlHelp(list);
            IList<IXmlCommentHelpParagraph> foundObjects = 
                sutHelpObject.LookUp($"TESTPARAGRAPH1", StringComparison.OrdinalIgnoreCase);
            
            Assert.AreEqual(1, foundObjects.Count);
        }
        
        [TestMethod]
        public void LookUp_WithTwoMatchingContentInCollection_ShouldReturnMatchingItems()
        {
            string entryNamePattern = "TestParagraph";

            IList<IXmlCommentHelpParagraph> list = CreateListWithMockContent(entryNamePattern, 10);
            list.Add(list[1]);
            
            XmlHelp sutHelpObject = new XmlHelp(list);
            IList<IXmlCommentHelpParagraph> foundObjects = 
                sutHelpObject.LookUp($"{entryNamePattern}1");
            
            Assert.AreEqual(2, foundObjects.Count);
            Assert.AreEqual($"{entryNamePattern}1", foundObjects[0].Name);
            Assert.AreEqual($"{entryNamePattern}1", foundObjects[1].Name);
        }
        
        [TestMethod]
        public void LookUp_WithNoMatchingContentInCollection_ShouldReturnEmptyCollection()
        {
            string entryNamePattern = "TestParagraph";

            IList<IXmlCommentHelpParagraph> list = CreateListWithMockContent(entryNamePattern, 10);
            list.Add(list[1]);
            
            XmlHelp sutHelpObject = new XmlHelp(list);
            IList<IXmlCommentHelpParagraph> foundObjects = 
                sutHelpObject.LookUp($"{entryNamePattern}99");
            
            Assert.AreEqual(0, foundObjects.Count);
        }

        private IList<IXmlCommentHelpParagraph> CreateListWithMockContent(string itemNamePattern, int itemCount)
        {
            IList<IXmlCommentHelpParagraph> createdList = new List<IXmlCommentHelpParagraph>();

            for (int i = 0; i < itemCount; i++)
            {
                Mock<IXmlCommentHelpParagraph> mockedListEntry = new Mock<IXmlCommentHelpParagraph>();
                mockedListEntry.Setup(le => le.Name).Returns($"{itemNamePattern}{i}");
                
                createdList.Add(mockedListEntry.Object);
            }
            return createdList;
        }
    }
}