using System;
using System.Threading;

namespace OS_Practice3
{
    internal static class Program
    {
        private static void Main()
        {
            DataQueue queue = new DataQueue(200);
            Producer producer = new Producer(queue);
            Consumer consumer = new Consumer(queue);

            Thread t1 = new Thread(producer.Start)
            {
                Name = "1"
            };
            Thread t2 = new Thread(producer.Start)
            {
                Name = "2"
            };
            Thread t3 = new Thread(producer.Start)
            {
                Name = "3"
            };
            Thread t4 = new Thread(consumer.Run)
            {
                Name = "4"
            };
            Thread t5 = new Thread(consumer.Run)
            {
                Name = "5"
            };

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();

            Thread.Sleep(5000);

            producer.Shutdown();

            t1.Join();
            t2.Join();
            t3.Join();

            consumer.Shutdown();

            t4.Join();
            t5.Join();


            Console.WriteLine("Finish!");
        }
    }
}
