public class Program
{
    static int counter = 0;
    static Object locker = new Object();
    public static void Main()
    {
        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine($"Final counter value is:{counter}");
        Console.ReadKey();
    }

    public static void IncrementCounter()
    {
        for (int i = 0; i < 100000; i++)
        {
            lock (locker)
            {
                counter = counter + 1;
                //var temp = counter;
                //counter = temp + 1;
            }
        }
    }
}

//We use a lock to avoid issues like in the previous example.
//Look in 006_Threads_Synchronization Program.cs