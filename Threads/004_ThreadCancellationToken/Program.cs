public class CancellationTokenProgram
{
    public static void Main()
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        //Pass the CancellationToken and the number-to-count into the operation
        ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1000));

        Thread.Sleep(5000);
        cts.Cancel();
        Console.ReadLine();
    }

    private static void Count(CancellationToken token, int countTo)
    {
        for (int i = 0; i < countTo; i++)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Count is cancelled");
                break; // Exit the loop to stop the operation
            }

            Console.WriteLine(i);
            Thread.Sleep(200); // For demo, waste some time
        }

        Console.WriteLine("Count is done");
    }
}