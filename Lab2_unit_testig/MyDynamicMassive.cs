using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab2_unit_testing
{
    

    public class MyDynamicMassive<T> : IList<T>
    {

        private const int DefaultCapacity = 4;
        private T[] _items;
        private int _capacity;
        private int _size;

        public event EventHandler<EventInfo<T>>? CollectionCleared;
        public event EventHandler<EventInfo<T>>? ItemAdded;
        public event EventHandler<EventInfo<T>>? ItemRemoved;
        public event EventHandler<EventInfo<T>>? ItemAddedToBeginning;
        public event EventHandler<EventInfo<T>>? ItemAddedToEnd;
        public event EventHandler<EventInfo<T>>? ItemRemovedAt;
        public event EventHandler<EventInfo<T>>? ContainsCheck;
        public event EventHandler<EventInfo<T>>? IndexOfCheck;


        public MyDynamicMassive(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _capacity = capacity;
            _size = 0;

            _items = capacity is 0
                ? Array.Empty<T>()
                : new T[capacity];
        }

        public int Count => _size;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _size)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _size)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
                _items[index] = value;
            }
        }


        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }
            if (_size >= _capacity)
            {
                Resize();
            }

            _items[_size] = item;
            _size++;

            ItemAdded?.Invoke(this, new EventInfo<T>("Add", item));
        }


        public void Clear()
        {
            if (_items == null)
            {
                throw new NullReferenceException("_items array is null.");
            }
            _items = new T[DefaultCapacity];
            _capacity = _size = 0;

            CollectionCleared?.Invoke(this, new EventInfo<T>("Clear", default(T)));
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Massive is null.");
            }
            var contains = false;
            for (int i = 0; i < _size; i++)
            {
                var element = _items[i];
                if (Equals(element, item))
                {
                    contains = true;
                    break;
                }
            }

            ContainsCheck?.Invoke(this, new EventInfo<T>("Contains", item));
            return contains;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Destination array cannot be null.");
            }

            if (array.Length - arrayIndex < _size)
            {
                throw new ArgumentException("Dest array is too small");
            }

            Array.Copy(_items, 0, array, arrayIndex, _size);
        }


        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }
            var index = Array.IndexOf(_items, item);
            var isRemoved = index != -1;
            RemoveAt(index);

            ItemRemoved?.Invoke(this, new EventInfo<T>("Remove", item));

            return isRemoved;
        }

        public int IndexOf(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }
            var index = Array.IndexOf(_items, item);

            IndexOfCheck?.Invoke(this, new EventInfo<T>("IndexOf", item));

            return index;
        }

        public void Insert(int index, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }

            if (index < 0 || index > _size)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index");
            }

            if (_size == _capacity)
            {
                Resize();
            }

            Array.Copy(_items, index, _items, index + 1, _size - index);

            _items[index] = item;
            _size++;

            ItemAdded?.Invoke(this, new EventInfo<T>("Insert", item));
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index.");
            }

            var removedItem = _items[index];
            _size--;
            Array.Copy(_items, index + 1, _items, index, _size - index);

            ItemRemovedAt?.Invoke(this, new EventInfo<T>("RemoveAt", removedItem));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private void Resize()
        {
            var newCapacity = (_capacity) * 2;
            var tempArray = new T[newCapacity];

            Array.Copy(_items, tempArray, _size);
            _items = tempArray;
            _capacity = newCapacity;
        }
        public class MyEnumerator : IEnumerator<T>
        {

            private MyDynamicMassive<T> _list;
            private int _cursor;


            public T Current
            {
                get
                {
                    if (_cursor >= 0 && _cursor < _list._size)
                    {
                        return _list._items[_cursor];
                    }
                    throw new InvalidOperationException();
                }
            }

            object IEnumerator.Current => Current;



            public MyEnumerator(MyDynamicMassive<T> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException(nameof(list), "List cannot be null.");
                }

                _list = list;
                _cursor = -1;
            }

            public void Dispose()
            {
                ((IEnumerator)this).Reset();
            }

            public bool MoveNext()
            {
                if (_cursor < _list.Count - 1)
                {
                    _cursor++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _cursor = -1;
            }
            
        }
    }
}
