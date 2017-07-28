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
            var task1 = new Task(() => ProcessMessage("Create Meeting"));     <--- 1
            task1.Start();      <--- 2

            Task.Run(() => ProcessMessage("Accept Meeting"));   <--- 3

            Task.Factory.StartNew(() => ProcessMessage("Reject Meeting"));  <--- 4
            Task.Factory.StartNew(() => ProcessMessage("Find an empty slot for all members of a team"), TaskCreationOptions.LongRunning);   <--- 5
            
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

1. Create a task and pass in a lambda expression to an *Action* delegate.

```cs
namespace System.Threading.Tasks {
    public class Task : IThreadPoolWorkItem, IAsyncResult, IDisposable {
        public Task(Action action);       <---- 
        public Task(Action action, CancellationToken cancellationToken);
        public Task(Action action, TaskCreationOptions creationOptions);
        public Task(Action<object> action, object state);
        public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions);
        public Task(Action<object> action, object state, CancellationToken cancellationToken);
        public Task(Action<object> action, object state, TaskCreationOptions creationOptions);
        public Task(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions);
```

2. Call the *Start()* method on the task to start executing the task.

3. The *Task.Run()* method 

```cs
namespace System.Threading.Tasks
{
    public class Task : IThreadPoolWorkItem, IAsyncResult, IDisposable 
    {
        //...
        
        public static Task Run(Func<Task> function);
        public static Task Run(Action action);      <---
        public static Task Run(Func<Task> function, CancellationToken cancellationToken);
        public static Task Run(Action action, CancellationToken cancellationToken);
        public static Task<TResult> Run<TResult>(Func<Task<TResult>> function);
        public static Task<TResult> Run<TResult>(Func<TResult> function);
        public static Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken);
        public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken);

    }
}
```

NOTE: It could be noticed from the output that the task execution order is not defined. 

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
