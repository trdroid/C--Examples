### Project Creation

*Program.cs*

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskOperation_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessMessage("Create Meetings");
            Console.WriteLine("--------------------");

            Task<bool> task = new Task<bool>(() => ProcessMessage("Create Meetings"));
            Console.WriteLine($"Task Status is {task.Status}");
            task.RunSynchronously();
            Console.WriteLine($"Task Status is {task.Status}");
            bool result = task.Result;
            Console.WriteLine($"Task Status is {task.Status}");

            Console.WriteLine($"Result is {result}");
            Console.WriteLine("--------------------");


            task = new Task<bool>(() => ProcessMessage("Delete Meetings"));
            Console.WriteLine($"Task Status is {task.Status}");
            task.Start();
            Console.WriteLine($"Task Status is {task.Status}");
            result = task.Result;
            Console.WriteLine($"Task Status is {task.Status}");

            Console.WriteLine($"Result is {result}");
            Console.WriteLine("--------------------");

            task = new Task<bool>(() => ProcessMessage("Cancel Meetings"));
            Console.WriteLine($"Task Status is {task.Status}");
            task.Start();
            Console.WriteLine($"Task Status is {task.Status}");

            Console.WriteLine("Entering the loop ... Task Completed is " + task.IsCompleted);            

            while (!task.IsCompleted)
            {
                Console.WriteLine($"Task Status is: {task.Status}");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine("Exited the loop ... Task Completed is " + task.IsCompleted);

            Console.WriteLine($"Task Status is {task.Status}");
            result = task.Result;
            Console.WriteLine($"Task Status is {task.Status}");

            Console.WriteLine($"Result is {result}");
            Console.WriteLine("--------------------");


            Console.WriteLine("Done");
            Console.ReadKey();
        }

        static bool ProcessMessage(string message)
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

            Thread.Sleep(TimeSpan.FromSeconds(10));
            bool operationSuccess = true;

            return operationSuccess;
        }     
    }
}
```

```sh
Processing the message 'Create Meetings' ... It might take a while ...
Message 'Create Meetings' is being processing by thread with id 9
The thread 9 is NOT from the thread pool
--------------------
Task Status is Created
Processing the message 'Create Meetings' ... It might take a while ...
Message 'Create Meetings' is being processing by thread with id 9
The thread 9 is NOT from the thread pool
Task Status is RanToCompletion
Task Status is RanToCompletion
Result is True
--------------------
Task Status is Created
Task Status is WaitingToRun
Processing the message 'Delete Meetings' ... It might take a while ...
Message 'Delete Meetings' is being processing by thread with id 6
The thread 6 is from the thread pool
Task Status is RanToCompletion
Result is True
--------------------
Task Status is Created
Task Status is WaitingToRun
Entering the loop ... Task Completed is False
Task Status is: Running
Processing the message 'Cancel Meetings' ... It might take a while ...
Message 'Cancel Meetings' is being processing by thread with id 10
The thread 10 is from the thread pool
Task Status is: Running
Task Status is: Running
Task Status is: Running
Task Status is: Running
Task Status is: Running
Task Status is: Running
Task Status is: Running
Task Status is: Running
Task Status is: Running
Exited the loop ... Task Completed is True
Task Status is RanToCompletion
Task Status is RanToCompletion
Result is True
--------------------
Done
```
