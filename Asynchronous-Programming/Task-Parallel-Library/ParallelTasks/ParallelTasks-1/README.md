### Project Creation

*Program.cs*

```cs
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
```

**Output**

```sh
12/28/2016 6:34:04 PM
Processing the message 'Delete Meetings' ... It might take a while ...
Processing the message 'Cancel Meetings' ... It might take a while ...
Message 'Cancel Meetings' is being processing by thread with id 13
The thread 13 is from the thread pool
Message 'Delete Meetings' is being processing by thread with id 12
The thread 12 is from the thread pool
Processing the message 'Create Meetings' ... It might take a while ...
Message 'Create Meetings' is being processing by thread with id 10
The thread 10 is from the thread pool
12/28/2016 6:34:15 PM
Results of the tasks are ...
5
3
4
```
