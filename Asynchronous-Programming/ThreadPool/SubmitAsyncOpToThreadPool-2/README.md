### Project

```cs
using System;
using System.Threading;

namespace SubmitAsyncOpToThreadPool_2 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Start of the Main thread {0}", Thread.CurrentThread.ManagedThreadId);
            ThreadPool.QueueUserWorkItem(AsyncOperation, "Idx");      <--- 1
            Thread.Sleep(TimeSpan.FromSeconds(15));
            Console.WriteLine("End of the Main thread {0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        private static void AsyncOperation(object operationId) {      <--- 2
            Console.WriteLine($"Performing Operation {(operationId == null ? "N/A" : operationId)} on Worker Thread {Thread.CurrentThread.ManagedThreadId} of the ThreadPool");
            Thread.Sleep(TimeSpan.FromSeconds(8));
            Console.WriteLine($"End of Async Operation {(operationId == null ? "N/A" : operationId)} on Worker Thread {Thread.CurrentThread.ManagedThreadId} of the ThreadPool");
        }
    }
}
```

### Output

```
Start of the Main thread 9
Performing Operation Idx on Worker Thread 10 of the ThreadPool
End of Async Operation Idx on Worker Thread 10 of the ThreadPool
End of the Main thread 9
```

### Explanation

1. Passing the value to the state parameter.
2. The state parameter

