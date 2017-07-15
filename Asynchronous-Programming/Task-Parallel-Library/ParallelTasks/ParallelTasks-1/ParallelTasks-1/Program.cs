using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTasks_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> taskA = new Task<int>(() => ProcessMessage("Create Meetings", 5));
            Task<int> taskB = new Task<int>(() => ProcessMessage("Delete Meetings", 3));
            Task<int> taskC = new Task<int>(() => ProcessMessage("Cancel Meetings", 4));

            Task<int[]> combinedTask = Task.WhenAll(taskA, taskB, taskC);            

            combinedTask.ContinueWith(results =>
            {
                Console.WriteLine(DateTime.Now);

                Console.WriteLine("Results of the tasks are ...");

                foreach (int result in results.Result)
                {
                    Console.WriteLine(result);
                }

            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            Console.WriteLine(DateTime.Now);

            taskA.Start();
            taskB.Start();
            taskC.Start();            

            Console.ReadKey();
        }

        static int ProcessMessage(string message, int processingUnits)
        {
            Console.WriteLine($"Processing the message '{message}' ... It might take a while ...");

            Console.WriteLine($"Message '{message}' is being processing by thread with id {Thread.CurrentThread.ManagedThreadId}");

            if (Thread.CurrentThread.IsThreadPoolThread)
            {
                Console.WriteLine($"The thread {Thread.CurrentThread.ManagedThreadId} is from the thread pool");
            }
            else
            {
                Console.WriteLine($"The thread {Thread.CurrentThread.ManagedThreadId} is NOT from the thread pool");
            }

            Thread.Sleep(TimeSpan.FromSeconds(2 * processingUnits));

            return processingUnits;
        }

    }
}
