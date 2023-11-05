public class ThreadsProgram
{
    public static void Main()
    {
        Thread thread = new Thread(Worker);
        thread.IsBackground = false;
        thread.Start();
        Console.WriteLine("Returning from Main");

        Console.WriteLine(new string('*', 30));
        Thread.Sleep(7000);
        Console.WriteLine(new string('-', 30));

        Console.WriteLine("Main thread: queuing an asynchronous operation");
        ThreadPool.QueueUserWorkItem(ComputeBoundOp1, 1);
        ThreadPool.QueueUserWorkItem(ComputeBoundOp2, 2);
        Console.WriteLine("Main thread: Doing other work here...");
        Thread.Sleep(10000);
        Console.WriteLine("Hit <Enter> to end this program...");
        Console.ReadLine();
    }

    private static void Worker()
    {
        Console.WriteLine("Starting Worker");
        Thread.Sleep(5000);
        Console.WriteLine("Returning from Worker");
    }

    private static void ComputeBoundOp1(Object state)
    {
        Console.WriteLine("In ComputeBoundOp1: state={0}", state);
        Thread.Sleep(2000);
        Console.WriteLine("Returning from ComputeBoundOp1");
    }
    private static void ComputeBoundOp2(Object state)
    {
        Console.WriteLine("In ComputeBoundOp2: state={0}", state);
        Thread.Sleep(1000);
        Console.WriteLine("Returning from ComputeBoundOp2");
    }
}