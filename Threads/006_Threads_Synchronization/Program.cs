public class Program
{
    static int counter = 0;
    public static void Main()
    {
        Console.WriteLine("Hi");

        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine($"Final counter value is:{counter}");

        Console.ReadLine();
    }

    public static void IncrementCounter()
    {
        for (int i = 0; i < 100000; i++) 
        {
            counter = counter + 1;
            //var temp = counter;
            //counter = temp + 1;
        }
    }
}

//When we run this code, we see that the counter has a different value each time. 
//This is because the compiler translates the line counter = counter + 1 into the 
//commented part of the code. If more than one thread works with that code, there 
//can be problems with shared resources like the counter.