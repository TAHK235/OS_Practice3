using System;
using System.Threading;

namespace OS_Practice3
{
    internal sealed class DataQueue
    {
        private int _head;
        private int _tail;
        private volatile int _elementsCount;
        private readonly int[] _myArrayQueue;

        internal DataQueue(int size)
        {
            _myArrayQueue = new int[size];
        }

        internal void Add(int element)
        {
            lock (this)
            {
                while (_elementsCount >= 100)
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                _myArrayQueue[_head] = element;
                _elementsCount++;

                if (_head == _myArrayQueue.Length - 1)
                    _head = 0;
                else
                    _head++;

                Monitor.PulseAll(this);
            }
        }

        internal void Remove()
        {
            lock (this)
            {
                while (GetElementsCount() == 0)
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                _myArrayQueue[_tail] = default;
                _elementsCount--;

                if (_tail == _myArrayQueue.Length - 1)
                    _tail = 0;
                else
                    _tail++;

                if (_elementsCount <= 80) Monitor.PulseAll(this);
            }
        }

        internal int GetElementsCount()
        {
            return _elementsCount;
        }
    }
}
