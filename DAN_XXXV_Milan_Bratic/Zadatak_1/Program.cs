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

        /// <summary>
        /// method for generatin threads
        /// </summary>
        /// <param name="players">pass number of players</param>
        /// <param name="number">pass guess number</param>
        /// <returns>list of threads</returns>
        static List<Thread> Thread_Generator(uint players, uint number)
        {
            List<Thread> threadList = new List<Thread>();
            try
            {
                for (int i = 0; i < players; i++)
                {
                    //create threads
                    Thread t = new Thread(() => GuessingNumber(number));
                    //generate thread name
                    t.Name = "Player_" + (i + 1);
                    //adding to the list
                    threadList.Add(t);

                }
            }
            catch (System.OutOfMemoryException)
            {
                Console.WriteLine("Error");
            }
            catch(Exception)
            {
                Console.WriteLine("Error");
            }

            return threadList;

        }
        /// <summary>
        /// method for guessing number
        /// </summary>
        static void GuessingNumber(uint number)
        {
            //loop rotate until guess number
            while (number != guessNumber)
            {

                lock (locker)
                {
                    Thread.Sleep(100);
                    //generate random number
                    guessNumber = rnd.Next(1, 101);

                    Console.WriteLine(Thread.CurrentThread.Name + " tried with the number " + guessNumber);
                    //if contidion for parity
                    if (number % 2 == 0 && guessNumber % 2 == 0 || number % 2 != 0 && guessNumber % 2 != 0)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " hit the parity of the number");
                    }
                    //if guess number
                    if (guessNumber == number)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " win. He hit required number.");
                        Console.WriteLine("Press enter to exit");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

            }
        }
        /// <summary>
        /// Method for start game, ask user how much players play and what is guess number
        /// </summary>
        /// <returns>array of two element that represent players and guess number</returns>
        static uint[] StartGame()
        {
            uint[] array = new uint[2];
            Console.WriteLine("Input the number of players: ");
            string input = null;
            uint players = 0;
            //validations
            do
            {
                input = Console.ReadLine();
                if (!(uint.TryParse(input, out players) && players > 0))
                {
                    Console.WriteLine("Wrong entry! Try again.");
                }
            } while (!(uint.TryParse(input, out players) && players > 0));
            Console.WriteLine("Input the number to be guessed: ");
            string input2 = null;
            uint number = 0;
            //validations
            do
            {
                input2 = Console.ReadLine();
                if (!(uint.TryParse(input2, out number) && number > 0 && number < 101))
                {
                    Console.WriteLine("Wrong entry! Number must be between 1 and 100. Try again.");
                }
            } while (!(uint.TryParse(input2, out number) && number > 0 && number < 101));
            array[0] = players;
            array[1] = number;
            return array;
        }
        static void Main(string[] args)
        {
            uint[] array = StartGame();

            Thread thread = new Thread(() => Thread_Generator(array[0], array[1]));
            thread.Start();
            Console.WriteLine("Number of players {0}, guess number is {1}", array[0], array[1]);
            List<Thread> list = Thread_Generator(array[0], array[1]);
            //start all threads
            foreach (Thread t in list)
            {
                t.Start();
            }
            Console.ReadKey();

        }
    }
}
