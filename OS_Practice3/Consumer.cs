using System;
using System.Threading;

namespace OS_Practice3
{
    internal sealed class Consumer
    {
        private readonly DataQueue _queue;
        private volatile bool _ready;

        public Consumer(DataQueue queue)
        {
            _queue = queue;
        }

        public void Run()
        {
            Random rand = new Random();

            while (!(_ready && _queue.GetElementsCount() == 0))
            {
                try
                {
                    Thread.Sleep(rand.Next(1000));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _queue.Remove();
                Console.WriteLine($"Queue elements size is: {_queue.GetElementsCount()}");
            }

            Console.WriteLine($"                Ending Thread: {Thread.CurrentThread.Name}");
        }

        internal void Shutdown()
        {
            _ready = true;
        }
    }
}
