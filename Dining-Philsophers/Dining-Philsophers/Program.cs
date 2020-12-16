using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dining_Philsophers
{
    class Program
    {
        static readonly bool[] Forks = { false, false, false, false, false };

        private static readonly object baton = new object();

        static void Main(string[] args)
        {
            Thread philosopherOne = new Thread(Eat) { Name = "Philosofer 1" };
            Thread philosopherTwo = new Thread(Eat) { Name = "Philosofer 2" };
            Thread philosopherTree = new Thread(Eat) { Name = "Philosofer 3" };
            Thread philosopherFour = new Thread(Eat) { Name = "Philosofer 4" };
            Thread philosopherFive = new Thread(Eat) { Name = "Philosofer 5" };

            philosopherOne.Start(new ForksBelongingsModel { Left = 1, Right = 0 });
            philosopherTwo.Start(new ForksBelongingsModel { Left = 2, Right = 1 });
            philosopherTree.Start(new ForksBelongingsModel { Left = 3, Right = 2 });
            philosopherFour.Start(new ForksBelongingsModel { Left = 4, Right = 3 });
            philosopherFive.Start(new ForksBelongingsModel { Left = 0, Right = 4 });

            Console.ReadLine();
        }

        private static void Eat(object input)
        {
            Random ran = new Random();
            ForksBelongingsModel forks = (ForksBelongingsModel)input;

            do
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is Thinking.........!");
                Thread.Sleep(ran.Next(10000));

                lock (baton)
                {
                    while (Forks[forks.Left] || Forks[forks.Right]) Monitor.Wait(baton);
                    Forks[forks.Left] = true;
                    Forks[forks.Right] = true;
                    Monitor.PulseAll(baton);
                }

                Console.WriteLine($"{Thread.CurrentThread.Name} Is eating.....!");
                Thread.Sleep(ran.Next(10000));
                Console.WriteLine($"{Thread.CurrentThread.Name} Has finished eating............! ");

                Forks[forks.Left] = false;
                Forks[forks.Right] = false;

            } while (true);
        }
    }

    public class ForksBelongingsModel
    {
        public int Left { get; set; }
        public int Right { get; set; }
    }
}
