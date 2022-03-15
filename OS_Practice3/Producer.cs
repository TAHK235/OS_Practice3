using System;
using System.Threading;

namespace OS_Practice3
{
    internal sealed class Producer
    {
        private readonly DataQueue _queue;
        private volatile bool _ready;

        public Producer(DataQueue queue)
        {
            _queue = queue;
        }

        public void Start()
        {
            Random rand = new Random();
            while (!_ready)
            {
                try
                {
                    Thread.Sleep(rand.Next(1000));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _queue.Add(rand.Next(1, 100));
                Console.WriteLine($"Queue elements size is: {_queue.GetElementsCount()}");
            }

            Console.WriteLine($"                Ending Thread: {Thread.CurrentThread.Name}");
        }

        public void Shutdown()
        {
            _ready = true;
        }
    }
}
