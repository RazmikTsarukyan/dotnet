using System.Threading;

public class ThereadPoolProgram
{
    public static void Main()
    {
        Console.WriteLine("Main thred: queuing an aynchronous operation");
        ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5);
        Console.WriteLine("Main thread: Doing other work here...");
        Thread.Sleep(10000); // Simulating other work (10 second)
        Console.WriteLine("Hit <Enter> to end this program");
        Console.ReadLine();
    }

    // This method's signature must match the WaitCallback delegate
    private static void ComputeBoundOp(Object state)
    {
        // This method is executed by a thread pool thread
        Thread.Sleep(5000); // Simulate other work (5 second)
        Console.WriteLine("In ComputeBoundOp: state={0}", state);
        Thread.Sleep(1000); // Simulate other work (1 second)

        // When this method returns, the thread goes back
        // to the pool and waits for another task
    }
}

//Creating and destroying a thread is an expensive operation in terms 
//of time. In addition, having lots of threads wastes memory resources and also hurts performance due 
//to the operating system having to schedule and context switch between the runnable threads. To 
//improve this situation, the CLR contains code to manage its own thread pool. You can think of a thread 
//pool as being a set of threads that are available for your application’s own use. There is one thread 
//pool per CLR; this thread pool is shared by all AppDomains controlled by that CLR. If multiple CLRs 
//load within a single process, then each CLR has its own thread pool. 

//When the CLR initializes, the thread pool has no threads in it. Internally, the thread pool maintains a 
//queue of operation requests. When your application wants to perform an asynchronous operation, you 
//call some method that appends an entry into the thread pool’s queue. The thread pool’s code will 
//extract entries from this queue and dispatch the entry to a thread pool thread. If there are no threads 
//in the thread pool, a new thread will be created. Creating a thread has a performance hit associated 
//with it. However, when a thread pool thread has completed its task, the thread is
//not destroyed; instead, the thread is returned to the thread pool, where it sits idle waiting to respond 
//to another request. Since the thread doesn’t destroy itself, there is no added performance hit.