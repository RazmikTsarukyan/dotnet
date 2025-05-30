using System.Threading;

class Program
{
    private static SemaphoreSlim semaphore = new SemaphoreSlim(3, 3);
    public static void Main()
    {
        for (int i = 1; i <= 10; i++)
        {
            int threadNumber = i;
            Thread thread = new Thread(() => AccessResource(threadNumber));
            thread.Start();
        }
    }

    static void AccessResource(int threadNumber)
    {
        Console.WriteLine($"Thread {threadNumber} is waiting to enter...");

        semaphore.Wait();
        try
        {
            Console.WriteLine($"Thread {threadNumber} has entered the semaphore.");
            Thread.Sleep(2000); // Simulate some work
            Console.WriteLine($"Thread {threadNumber} is leaving the semaphore.");
        }
        finally
        {
            semaphore.Release();
        }
    }
}

//We use a semaphore to control access to shared resources in multi-threaded programs. It helps limit the number of threads that
//can access a resource at the same time. A semaphore is used when we need to synchronize across processes or when working with
//legacy code that uses Thread. We use SemaphoreSlim when only in-process synchronization is needed or when using async/ await.