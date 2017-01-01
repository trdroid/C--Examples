### Project Creation

```sh
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
```

### Output 

```sh
Time Now: 12/31/2016 20:14:47.491
Processing the message 'Create Meetings' ... It might take a while ...
Message 'Create Meetings' is being processing by thread with id 12
The thread 12 is from the thread pool
Processing the message 'Create Meetings' ... It might take a while ...
Message 'Create Meetings' is being processing by thread with id 10
The thread 10 is from the thread pool
Processing the message 'Create Meetings' ... It might take a while ...
Message 'Create Meetings' is being processing by thread with id 11
The thread 11 is from the thread pool
Processing the message 'Create Meetings' ... It might take a while ...
Message 'Create Meetings' is being processing by thread with id 13
The thread 13 is from the thread pool
Processing the message 'Create Meetings' ... It might take a while ...
Message 'Create Meetings' is being processing by thread with id 14
The thread 14 is from the thread pool
A task has completed with result: 6, Time Now: 12/31/2016 20:14:59.933
A task has completed with result: 6, Time Now: 12/31/2016 20:14:59.934
A task has completed with result: 6, Time Now: 12/31/2016 20:14:59.935
A task has completed with result: 6, Time Now: 12/31/2016 20:14:59.943
A task has completed with result: 6, Time Now: 12/31/2016 20:15:00.590
Done
```
