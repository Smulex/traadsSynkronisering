using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace trådSynkronisering2And3
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter();

            Thread threadStar = new Thread(c.WriteStar);
            Thread threadHash = new Thread(c.WriteHash);

            threadStar.Start();
            threadHash.Start();

            Console.ReadKey();
        }

    }
    class Counter
    {
        object locker = new object();
        int count = 0;

        public void WriteStar()
        {
            while (true)
            {
                lock (locker)
                {
                    for (int i = 0; i < 60; i++)
                    {
                        Console.Write("*");
                        count++;
                    }
                    Console.WriteLine(count);
                }
            }
        }
        public void WriteHash()
        {
            while (true)
            {
                lock (locker)
                {
                    for (int i = 0; i < 60; i++)
                    {
                        Console.Write("#");
                        count++;
                    }
                    Console.WriteLine(count);
                }
            }
        }
    }
}

