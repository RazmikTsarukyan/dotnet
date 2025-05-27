class Program
{
    static int counter;
    static object counterLock = new object();
    public static void Main()
    {
        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine($"Final counter value is:{counter}");
    }

    static void IncrementCounter()
    {
        for (int i = 0; i < 1000000; i++) 
        {
            Monitor.Enter(counterLock);
            try
            {
                counter = counter + 1;
            }
            finally
            {
                Monitor.Exit(counterLock);
            }
        }
    }
}

//The lock statement is a language-level shortcut (syntactic sugar) for using Monitor.Enter and Monitor.Exit.
//Instead of lock compiler generates code like IncrementCounter's monitor section with Monitor.Enter, try finally and Monitor.Exit.
//Monitor also provides additional functionality that is not available when using lock, such as Monitor.Enter, Monitor.Exit,
//Monitor.Wait, Monitor.Pulse, Monitor.PulseAll, and timeout support with Monitor.TryEnter