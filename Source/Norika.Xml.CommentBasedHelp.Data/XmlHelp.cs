using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Norika.Xml.CommentBasedHelp.Data.Interfaces;

namespace Norika.Xml.CommentBasedHelp.Data
{
    /// <summary>
    /// Implementation of the <see cref="IXmlHelp"/> interface providing the xml comment based
    /// help for an xml element. 
    /// </summary>
    public class XmlHelp : IXmlHelp
    {
        /// <summary>
        /// Contains the xml comment based help paragraphs
        /// </summary>
        private readonly IList<IXmlCommentHelpParagraph> _paragraphs = new List<IXmlCommentHelpParagraph>();
        
        /// <summary>
        /// <inheritdoc cref="IList.Count"/>
        /// </summary>
        public int Count => _paragraphs.Count;
        
        /// <summary>
        /// <inheritdoc cref="IList.IsReadOnly"/>
        /// </summary>
        public bool IsReadOnly => _paragraphs.IsReadOnly;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paragraphList">List to set as default value</param>
        public XmlHelp(IList<IXmlCommentHelpParagraph> paragraphList)
        {
            _paragraphs = paragraphList;
        }

        
        /// <summary>
        /// Constructor
        /// </summary>
        public XmlHelp(){}
        

        /// <summary>
        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        /// </summary>
        public IEnumerator<IXmlCommentHelpParagraph> GetEnumerator()
        {
            return _paragraphs.GetEnumerator();
        }

        /// <summary>
        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// <inheritdoc cref="IList.Add"/>
        /// </summary>
        public void Add(IXmlCommentHelpParagraph item)
        {
            _paragraphs.Add(item);
        }

        /// <summary>
        /// <inheritdoc cref="IList.Clear"/>
        /// </summary>
        public void Clear()
        {
            _paragraphs.Clear();
        }

        /// <summary>
        /// <inheritdoc cref="IList.Contains"/>
        /// </summary>
        public bool Contains(IXmlCommentHelpParagraph item)
        {
            return _paragraphs.Contains(item);
        }

        /// <summary>
        /// <inheritdoc cref="IList.CopyTo"/>
        /// </summary>
        public void CopyTo(IXmlCommentHelpParagraph[] array, int arrayIndex)
        {
            _paragraphs.CopyTo(array, arrayIndex);
        }
            
        /// <summary>
        /// <inheritdoc cref="IList.Remove"/>
        /// </summary>
        public bool Remove(IXmlCommentHelpParagraph item)
        {
            return _paragraphs.Remove(item);
        }

        /// <summary>
        /// <inheritdoc cref="IList.IndexOf"/>
        /// </summary>
        public int IndexOf(IXmlCommentHelpParagraph item)
        {
            return _paragraphs.IndexOf(item);
        }

        /// <summary>
        /// <inheritdoc cref="IList.Insert"/>
        /// </summary>
        public void Insert(int index, IXmlCommentHelpParagraph item)
        {
            _paragraphs.Insert(index, item);
        }

        /// <summary>
        /// <inheritdoc cref="IList.RemoveAt"/>
        /// </summary>
        public void RemoveAt(int index)
        {
            _paragraphs.RemoveAt(index);
        }

        /// <summary>
        /// Implementation of the array accessor.
        /// </summary>
        public IXmlCommentHelpParagraph this[int index]
        {
            get => _paragraphs[index];
            set => _paragraphs[index] = value;
        }

        /// <summary>
        /// Looks up if an paragraph with the given header title and
        /// returns all matching paragraphs.
        /// </summary>
        /// <param name="paragraphName">Paragraph header title</param>
        /// <returns>List of HelpParagraphs matching the given header title.</returns>
        public IList<IXmlCommentHelpParagraph> LookUp(string paragraphName)
        {
            return _paragraphs.Where(p => p.Name.Equals(paragraphName)).ToList();
        }

        /// <summary>
        /// Looks up if an paragraph with the given header title and
        /// returns all matching paragraphs.
        /// </summary>
        /// <param name="paragraphName">Paragraph header title</param>
        /// <param name="comparison">String comparision type for header title.</param>
        /// <returns>List of HelpParagraphs matching the given header title.</returns>
        public IList<IXmlCommentHelpParagraph> LookUp(string paragraphName, StringComparison comparison)
        {
            return _paragraphs.Where(p => p.Name.Equals(paragraphName, comparison)).ToList();
        }

        /// <summary>
        /// <inheritdoc cref="IXmlHelp.Remove(string,bool)"/>
        /// </summary>
        public bool Remove(string paragraphName, bool distinctOnly)
        {
            IList<IXmlCommentHelpParagraph> removeItems =
                _paragraphs.Where(x => x.Name.Equals(paragraphName)).ToList();

            return removeItems.Aggregate(true, (current, paragraph) => current && _paragraphs.Remove(paragraph));
        }
    }
}