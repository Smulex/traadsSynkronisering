using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace trådsSynkronisering1
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter();

            Thread threadUp = new Thread(c.CountUp);
            Thread threadDown = new Thread(c.CountDown);

            threadUp.Start();
            threadDown.Start();
        }

    }
    class Counter
    {
        int count = 0;

        public void CountUp()
        {
            while (true)
            {
                lock ((object)count)
                {
                    count += 2;
                    Console.WriteLine(count);
                }
                Thread.Sleep(1000);

            }
        }
        public void CountDown()
        {
            while (true)
            {
                lock ((object)count)
                {
                    count--;
                    Console.WriteLine(count);
                }
                Thread.Sleep(1000);
            }
        }
    }
}
