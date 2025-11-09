class Program
{
    // false = not signaled (threads will wait)
    // true  = signaled (threads will not wait)
    private static ManualResetEvent doneEvent = new ManualResetEvent(false);

    public static void Main()
    {
        Thread thread1 = new Thread(DoWork1);
        Thread thread2 = new Thread(DoWork2);

        thread2.Start();
        thread1.Start();

        thread1.Join();
        thread2.Join();
    }

    static void DoWork1()
    {
        Console.WriteLine("Work 1 starting...");
        Thread.Sleep(1000);
        Console.WriteLine("Work 1 finished");

        doneEvent.Set();
    }

    static void DoWork2()
    {
        Console.WriteLine("Work 2 waiting...");
        doneEvent.WaitOne();
        Console.WriteLine("Work 2 starting...");
    }
}

//ManualResetEvent is a synchronization primitive that allows threads to wait until another thread signals them to continue.
//We use ManualResetEventSlim when we need fast, lightweight, in-process thread signaling and the expected wait time is short.