using System;
using System.Threading;

namespace SubmitAsyncOpToThreadPool_2 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Start of the Main thread {0}", Thread.CurrentThread.ManagedThreadId);
            ThreadPool.QueueUserWorkItem(AsyncOperation, "Idx");
            Thread.Sleep(TimeSpan.FromSeconds(15));
            Console.WriteLine("End of the Main thread {0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        private static void AsyncOperation(object operationId) {
            Console.WriteLine($"Performing Operation {(operationId == null ? "N/A" : operationId)} on Worker Thread {Thread.CurrentThread.ManagedThreadId} of the ThreadPool");
            Thread.Sleep(TimeSpan.FromSeconds(8));
            Console.WriteLine($"End of Async Operation {(operationId == null ? "N/A" : operationId)} on Worker Thread {Thread.CurrentThread.ManagedThreadId} of the ThreadPool");
        }
    }
}
