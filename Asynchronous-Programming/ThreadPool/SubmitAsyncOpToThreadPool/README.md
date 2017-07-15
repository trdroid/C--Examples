### Project

```cs
using System;
using System.Threading;

namespace SubmitAsyncOpToThreadPool {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Start of the Main thread {0}", Thread.CurrentThread.ManagedThreadId);
            ThreadPool.QueueUserWorkItem(AsyncOperation);
            Thread.Sleep(TimeSpan.FromSeconds(15));
            Console.WriteLine("End of the Main thread {0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();        
        }

        private static void AsyncOperation(object operationId) {
            Console.WriteLine($"Performing Operation {(operationId == null? "N/A" : operationId)} on Worker Thread {Thread.CurrentThread.ManagedThreadId} of the ThreadPool");
            Thread.Sleep(TimeSpan.FromSeconds(8));
            Console.WriteLine($"End of Async Operation {(operationId == null ? "N/A" : operationId)} on Worker Thread {Thread.CurrentThread.ManagedThreadId} of the ThreadPool");
        }
    }
}
```

### Output

```
Start of the Main thread 9
Performing Operation N/A on Worker Thread 10 of the ThreadPool
End of Async Operation N/A on Worker Thread 10 of the ThreadPool
End of the Main thread 9
```

### Explanation

The main thread calls the Threadpool.QueueUserWorkItem() method by passing it the name of a method 'AsyncOperation' that has to be executed asynchronously
by a thread from the the thread pool. The main thread moves right on and puts itself to sleep for 15 seconds. 

The thread pool assigns a worker thread from the thread pool to execute the method 'AsyncOperation', 
which prints some messages to the console and sleeps for 8 seconds, wakes back up and finishes the rest of the execution. 
After the method is executed, the worker thread is put back to the thread pool for later usage. 

After waking up, the main thread finishes the rest of the execution and waits for the user input. 

If the main thread finishes execution before the worker thread, the worker thread is terminated. 



