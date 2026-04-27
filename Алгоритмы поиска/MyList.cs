using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритмы_поиска
{

    public class MyList<T>
    {
        private T[] items;
        private int count;
        private int capacity;

        public MyList()
        {
            capacity = 4;
            items = new T[capacity];
            count = 0;
        }

        public MyList(int initialCapacity)
        {
            capacity = initialCapacity;
            items = new T[capacity];
            count = 0;
        }

        public int Count { get { return count; } }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException();
                return items[index];
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException();
                items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (count >= capacity)
            {
                capacity *= 2;
                T[] newItems = new T[capacity];
                for (int i = 0; i < count; i++)
                    newItems[i] = items[i];
                items = newItems;
            }
            items[count] = item;
            count++;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
                throw new IndexOutOfRangeException();

            if (count >= capacity)
            {
                capacity *= 2;
                T[] newItems = new T[capacity];
                for (int i = 0; i < count; i++)
                    newItems[i] = items[i];
                items = newItems;
            }

            for (int i = count; i > index; i--)
                items[i] = items[i - 1];

            items[index] = item;
            count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            for (int i = index; i < count - 1; i++)
                items[i] = items[i + 1];

            count--;
        }

        public void Clear()
        {
            count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i] != null && items[i].Equals(item))
                    return true;
            }
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i] != null && items[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public void Reverse()
        {
            for (int i = 0; i < count / 2; i++)
            {
                T temp = items[i];
                items[i] = items[count - 1 - i];
                items[count - 1 - i] = temp;
            }
        }

        // Сортировка с компаратором (делегатом) - работает для любых типов
        public void Sort(Comparison<T> comparison)
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - 1 - i; j++)
                {
                    if (comparison(items[j], items[j + 1]) > 0)
                    {
                        T temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }

        public T[] ToArray()
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
                result[i] = items[i];
            return result;
        }

        public void Print(string separator = ", ")
        {
            Console.Write("[");
            for (int i = 0; i < count; i++)
            {
                Console.Write(items[i]);
                if (i < count - 1)
                    Console.Write(separator);
            }
            Console.Write("]");
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                result += items[i];
                if (i < count - 1)
                    result += ", ";
            }
            return result;
        }
    }
}

