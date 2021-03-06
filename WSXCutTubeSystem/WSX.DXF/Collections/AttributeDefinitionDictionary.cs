#region WSX.DXF library, Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using WSX.DXF.Entities;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represents a dictionary of <see cref="AttributeDefinition">AttributeDefinitions</see>.
    /// </summary>
    public sealed class AttributeDefinitionDictionary :
        IDictionary<string, AttributeDefinition>
    {
        #region delegates and events

        public delegate void BeforeAddItemEventHandler(AttributeDefinitionDictionary sender, AttributeDefinitionDictionaryEventArgs e);

        public event BeforeAddItemEventHandler BeforeAddItem;

        private bool OnBeforeAddItemEvent(AttributeDefinition item)
        {
            BeforeAddItemEventHandler ae = this.BeforeAddItem;
            if (ae != null)
            {
                AttributeDefinitionDictionaryEventArgs e = new AttributeDefinitionDictionaryEventArgs(item);
                ae(this, e);
                return e.Cancel;
            }
            return false;
        }

        public delegate void AddItemEventHandler(AttributeDefinitionDictionary sender, AttributeDefinitionDictionaryEventArgs e);

        public event AddItemEventHandler AddItem;

        private void OnAddItemEvent(AttributeDefinition item)
        {
            AddItemEventHandler ae = this.AddItem;
            if (ae != null)
                ae(this, new AttributeDefinitionDictionaryEventArgs(item));
        }

        public delegate void BeforeRemoveItemEventHandler(AttributeDefinitionDictionary sender, AttributeDefinitionDictionaryEventArgs e);

        public event BeforeRemoveItemEventHandler BeforeRemoveItem;

        private bool OnBeforeRemoveItemEvent(AttributeDefinition item)
        {
            BeforeRemoveItemEventHandler ae = this.BeforeRemoveItem;
            if (ae != null)
            {
                AttributeDefinitionDictionaryEventArgs e = new AttributeDefinitionDictionaryEventArgs(item);
                ae(this, e);
                return e.Cancel;
            }
            return false;
        }

        public delegate void RemoveItemEventHandler(AttributeDefinitionDictionary sender, AttributeDefinitionDictionaryEventArgs e);

        public event RemoveItemEventHandler RemoveItem;

        private void OnRemoveItemEvent(AttributeDefinition item)
        {
            RemoveItemEventHandler ae = this.RemoveItem;
            if (ae != null)
                ae(this, new AttributeDefinitionDictionaryEventArgs(item));
        }

        #endregion

        #region private fields

        private readonly Dictionary<string, AttributeDefinition> innerDictionary;

        #endregion

        #region constructor

        public AttributeDefinitionDictionary()
        {
            this.innerDictionary = new Dictionary<string, AttributeDefinition>(StringComparer.OrdinalIgnoreCase);
        }

        public AttributeDefinitionDictionary(int capacity)
        {
            this.innerDictionary = new Dictionary<string, AttributeDefinition>(capacity, StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region public properties

        public AttributeDefinition this[string tag]
        {
            get { return this.innerDictionary[tag]; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (!string.Equals(tag, value.Tag, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException(string.Format("The dictionary tag: {0}, and the attribute definition tag: {1}, must be the same", tag, value.Tag));

                // there is no need to add the same object, it might cause overflow issues
                if (ReferenceEquals(this.innerDictionary[tag].Value, value))
                    return;

                AttributeDefinition remove = this.innerDictionary[tag];
                if (this.OnBeforeRemoveItemEvent(remove))
                    return;
                if (this.OnBeforeAddItemEvent(value))
                    return;
                this.innerDictionary[tag] = value;
                this.OnAddItemEvent(value);
                this.OnRemoveItemEvent(remove);
            }
        }

        public ICollection<string> Tags
        {
            get { return this.innerDictionary.Keys; }
        }

        public ICollection<AttributeDefinition> Values
        {
            get { return this.innerDictionary.Values; }
        }

        public int Count
        {
            get { return this.innerDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region public methods

        public void Add(AttributeDefinition item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (this.OnBeforeAddItemEvent(item))
                throw new ArgumentException("The attribute definition cannot be added to the collection.", nameof(item));
            this.innerDictionary.Add(item.Tag, item);
            this.OnAddItemEvent(item);
        }

        public void AddRange(IEnumerable<AttributeDefinition> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            // we will make room for so the collection will fit without having to resize the internal array during the Add method
            foreach (AttributeDefinition item in collection)
                this.Add(item);
        }

        public bool Remove(string tag)
        {
            AttributeDefinition remove;
            if (!this.innerDictionary.TryGetValue(tag, out remove))
                return false;
            if (this.OnBeforeRemoveItemEvent(remove))
                return false;
            this.innerDictionary.Remove(tag);
            this.OnRemoveItemEvent(remove);
            return true;
        }

        public void Clear()
        {
            string[] tags = new string[this.innerDictionary.Count];
            this.innerDictionary.Keys.CopyTo(tags, 0);
            foreach (string tag in tags)
            {
                this.Remove(tag);
            }
        }

        public bool ContainsTag(string tag)
        {
            return this.innerDictionary.ContainsKey(tag);
        }

        public bool ContainsValue(AttributeDefinition value)
        {
            return this.innerDictionary.ContainsValue(value);
        }

        public bool TryGetValue(string tag, out AttributeDefinition value)
        {
            return this.innerDictionary.TryGetValue(tag, out value);
        }

        public IEnumerator<KeyValuePair<string, AttributeDefinition>> GetEnumerator()
        {
            return this.innerDictionary.GetEnumerator();
        }

        #endregion

        #region private properties

        ICollection<string> IDictionary<string, AttributeDefinition>.Keys
        {
            get { return this.innerDictionary.Keys; }
        }

        #endregion

        #region private methods

        bool IDictionary<string, AttributeDefinition>.ContainsKey(string tag)
        {
            return this.innerDictionary.ContainsKey(tag);
        }

        void IDictionary<string, AttributeDefinition>.Add(string key, AttributeDefinition value)
        {
            this.Add(value);
        }

        void ICollection<KeyValuePair<string, AttributeDefinition>>.Add(KeyValuePair<string, AttributeDefinition> item)
        {
            this.Add(item.Value);
        }

        bool ICollection<KeyValuePair<string, AttributeDefinition>>.Remove(KeyValuePair<string, AttributeDefinition> item)
        {
            if (!ReferenceEquals(item.Value, this.innerDictionary[item.Key]))
                return false;
            return this.Remove(item.Key);
        }

        bool ICollection<KeyValuePair<string, AttributeDefinition>>.Contains(KeyValuePair<string, AttributeDefinition> item)
        {
            return ((IDictionary<string, AttributeDefinition>) this.innerDictionary).Contains(item);
        }

        void ICollection<KeyValuePair<string, AttributeDefinition>>.CopyTo(KeyValuePair<string, AttributeDefinition>[] array, int arrayIndex)
        {
            ((IDictionary<string, AttributeDefinition>) this.innerDictionary).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}