using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static Random rnd = new Random();
        static int guessNumber;
        private static object locker = new object();

        static List<Thread> Thread_Generator(uint players, uint number)
        {
            List<Thread> threadList = new List<Thread>();
            for (int i = 0; i < players; i++)
            {
                Thread t = new Thread(() => GuessingNumber(number));
                t.Name = "Player_" + (i + 1);
                threadList.Add(t);
            }
            return threadList;

        }

        static void GuessingNumber(uint number)
        {
            
            while (number != guessNumber)
            {

                lock (locker)
                {
                    Thread.Sleep(100);
                    guessNumber = rnd.Next(1, 101);
                    
                    Console.WriteLine(Thread.CurrentThread.Name + " tried with the number " + guessNumber);
                    if (number % 2 == 0 && guessNumber % 2 == 0 || number % 2 != 0 && guessNumber % 2 != 0)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " hit the parity of the number");
                    }
                    if (guessNumber == number)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " win. He hit required number.");
                        
                        Console.WriteLine("If you want to exit, press enter");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Input the number of players: ");
            uint players = uint.Parse(Console.ReadLine());
            Console.WriteLine("Input the number to be guessed: ");
            uint number = uint.Parse(Console.ReadLine());

            Thread thread = new Thread(() => Thread_Generator(players, number));
            thread.Start();
            List<Thread> list = Thread_Generator(players, number);

            foreach (Thread t in list)
            {
                t.Start();
            }


            Console.ReadKey();

        }
    }
}
