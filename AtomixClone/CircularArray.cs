using System;
using System.Collections;
using System.Collections.Generic;

namespace AtomixClone
{
    public class CircularArray<T> : IEnumerable<T>
    {
        private readonly T[] circularArray;
        private int circularLength;

        public bool Full { get; private set; }

        public int Length
        {
            get { return circularArray.Length; }
        }

        public int CircularLength
        {
            get
            {
                return circularLength;
            }
            private set
            {
                circularLength = value;
                if (circularLength >= Length)
                {
                    Full = true;
                    circularLength %= Length;
                }
            }
        }

        public int ActualLength
        {
            get { return Full ? Length : CircularLength; }
        }

        public int FirstIndex
        {
            get
            {
                if (!Full)
                {
                    if (CircularLength == 0)
                        return -1;
                    else
                        return 0;
                }
                return CircularLength;
            }
        }

        public int LastIndex
        {
            get
            {
                if (!Full) return CircularLength - 1;
                return (CircularLength - 1) % Length;
            }
        }

        public CircularArray() : this(0) { }

        public CircularArray(int length)
        {
            circularArray = new T[length];
        }

        public T this[int index]
        {
            get { return circularArray[index]; }
        }

        public void Add(T item)
        {
            circularArray[CircularLength++] = item;
        }

        public void Clear()
        {
            Array.Clear(circularArray, 0, Length);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            int actualLength = ActualLength;
            int firstIndex = FirstIndex;
            for (int i = 0; i < actualLength; ++i)
                yield return circularArray[(firstIndex + i) % Length];
        }
    }
}
