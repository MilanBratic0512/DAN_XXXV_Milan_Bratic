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
        static void Thread_Generator(uint players, uint number)
        {
            List<Thread> threadList = new List<Thread>();
            for (int i = 0; i < players; i++)
            {
                Thread t = new Thread(() => GuessingNumber(number));
                threadList.Add(t);
            }
            
        }
       
        static void GuessingNumber(uint number)
        {

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Input the number of players: ");
            uint players = uint.Parse(Console.ReadLine());
            Console.WriteLine("Input the number to be guessed: ");
            uint number = uint.Parse(Console.ReadLine());

            Thread thread = new Thread(() => Thread_Generator(players, number));
            thread.Start();
            Thread[] threads = new Thread[players];

            Console.ReadKey();
            
        }
    }
}
