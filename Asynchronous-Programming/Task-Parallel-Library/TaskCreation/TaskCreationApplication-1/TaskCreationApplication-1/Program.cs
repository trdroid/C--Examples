using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaskCreationApplication_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var task1 = new Task(() => ProcessMessage("Create Meeting"));
            task1.Start();

            Task.Run(() => ProcessMessage("Accept Meeting"));

            Task.Factory.StartNew(() => ProcessMessage("Reject Meeting"));
            Task.Factory.StartNew(() => ProcessMessage("Find an empty slot for all members of a team"), TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.ReadKey();
        }

        static void ProcessMessage(string message)
        {
            Console.WriteLine($"Message {message} is being processing by thread with id {Thread.CurrentThread.ManagedThreadId}\n");

            if (Thread.CurrentThread.IsThreadPoolThread)
            {
                Console.WriteLine($"The thread is from the thread pool");
            }
            else
            {
                Console.WriteLine($"The thread is NOT from the thread pool");
            }
        }
    }
}
