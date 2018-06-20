using System;
using System.Collections;
using System.Collections.Generic;

namespace BlingRus.Domain.EnterpriseCollections
{
    public class EnterpriseListWrapper<T> : EnterpriseBag, IList<T>
    {
        private readonly IList<T> _wrappedList;

        public EnterpriseListWrapper(IList<T> wrappedList)
        {
            _wrappedList = wrappedList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnterpriseEnumerator<T>(_wrappedList);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            foreach (var thing in this)
            {
                if (item.Equals(thing))
                    throw new InvalidOperationException("Item already exists!");
            }
            
            _wrappedList.Add(item);
        }

        public void Clear()
        {
            _wrappedList.Clear();
        }

        public bool Contains(T item)
        {
            return _wrappedList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _wrappedList.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _wrappedList.Remove(item);
        }

        public int Count => _wrappedList.Count;
        public bool IsReadOnly => _wrappedList.IsReadOnly;
        public int IndexOf(T item)
        {
            return _wrappedList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _wrappedList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _wrappedList.RemoveAt(index);
        }

        public T this[int index]
        {
            get => _wrappedList[index];
            set => _wrappedList[index] = value;
        }
    }
}
