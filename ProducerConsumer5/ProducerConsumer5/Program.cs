using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer5
{
    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumer pc = new ProducerConsumer();
            Thread ProducerThread = new Thread(new ThreadStart(pc.Producer));
            Thread ConsumerThread = new Thread(new ThreadStart(pc.Consumer));

            ProducerThread.Start();
            ConsumerThread.Start();

            Console.ReadLine();
        }
    }

    class ProducerConsumer
    {
        private static readonly Queue<string> ProductBuffer = new Queue<string>(5);

        public void Producer()
        {
            while (true)
            {
                lock (ProductBuffer)
                {
                    for (int i = 0; i < 5 - ProductBuffer.Count; i++)
                    {
                        ProductBuffer.Enqueue("Product");
                        Console.WriteLine("Producer har produceret: " + ProductBuffer.Count);
                    }
                    Monitor.PulseAll(ProductBuffer);
                    Thread.Sleep(1000);
                }
            }
        }
        public void Consumer()
        {
            while (true)
            {
                lock (ProductBuffer)
                {
                    if (ProductBuffer.Count == 0)
                    {
                        Console.WriteLine("Producer waits...");
                        Monitor.Wait(ProductBuffer);
                    }

                    Console.WriteLine("Consumer har consumeret: " + ProductBuffer.Count);
                    ProductBuffer.Dequeue();
                }
            }
        }
    }
}
