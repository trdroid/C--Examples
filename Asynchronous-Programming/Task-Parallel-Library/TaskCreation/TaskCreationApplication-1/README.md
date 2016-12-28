### Project Contents

*TaskCreationApplication-1/TaskCreationApplication-1/Program.cs*

```cs
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
        }

        static void ProcessMessage(string message)
        {
            Console.WriteLine($"Message '{message}' is being processing by thread with id {Thread.CurrentThread.ManagedThreadId}");

            if (Thread.CurrentThread.IsThreadPoolThread)
            {
                Console.WriteLine($"The thread {Thread.CurrentThread.ManagedThreadId} is from the thread pool");
            }
            else
            {
                Console.WriteLine($"The thread {Thread.CurrentThread.ManagedThreadId} is NOT from the thread pool");
            }

            Console.WriteLine();
        }
    }
}
```


**Output**

The output from multiple runs is

```sh
Message 'Accept Meeting' is being processing by thread with id 11
The thread 11 is from the thread pool

Message 'Reject Meeting' is being processing by thread with id 11
The thread 11 is from the thread pool

Message 'Create Meeting' is being processing by thread with id 10
The thread 10 is from the thread pool

Message 'Find an empty slot for all members of a team' is being processing by thread with id 13
The thread 13 is NOT from the thread pool
```

```sh
Message 'Create Meeting' is being processing by thread with id 10
The thread 10 is from the thread pool

Message 'Accept Meeting' is being processing by thread with id 11
The thread 11 is from the thread pool

Message 'Reject Meeting' is being processing by thread with id 11
The thread 11 is from the thread pool

Message 'Find an empty slot for all members of a team' is being processing by thread with id 13
The thread 13 is NOT from the thread pool
```


```sh
Message 'Accept Meeting' is being processing by thread with id 11
The thread 11 is from the thread pool

Message 'Create Meeting' is being processing by thread with id 6
The thread 6 is from the thread pool

Message 'Reject Meeting' is being processing by thread with id 13
The thread 13 is from the thread pool

Message 'Find an empty slot for all members of a team' is being processing by thread with id 14
The thread 14 is NOT from the thread pool
```
