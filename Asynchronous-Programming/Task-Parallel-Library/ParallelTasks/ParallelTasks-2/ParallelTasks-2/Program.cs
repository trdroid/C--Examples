using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTasks_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task<int>> tasks = new List<Task<int>>();

            Console.WriteLine("Time Now: {0:MM/dd/yyy HH:mm:ss.fff}", DateTime.Now);

            for (int iter = 1; iter <= 5; iter++)
            {
                Task<int> task = new Task<int>(() => ProcessMessage("Create Meetings", iter));
                tasks.Add(task);
                task.Start();
            }

            while(tasks.Count > 0)
            {
                Task<int> completedTask = Task.WhenAny(tasks).Result;

                Console.WriteLine($"A task has completed with result: {completedTask.Result}" + ", Time Now: {0:MM/dd/yyy HH:mm:ss.fff}", DateTime.Now);

                tasks.Remove(completedTask);
            }            

            Console.WriteLine("Done");
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
