using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
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
        private static readonly Queue<string> ProductBuffer = new Queue<string>(3);

        public void Producer()
        {
            while (true)
            {
                if (ProductBuffer.Count == 3)
                {
                    Console.WriteLine("Producer fik ikke lov til at producere: " + ProductBuffer.Count);
                }
                else if (ProductBuffer.Count == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        ProductBuffer.Enqueue("Product");
                        Console.WriteLine("Producer har produceret: " + ProductBuffer.Count);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        public void Consumer()
        {
            while (true)
            {
                if (ProductBuffer.Count == 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine("Consumer har consumeret " + ProductBuffer.Count);
                        ProductBuffer.Dequeue();
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
