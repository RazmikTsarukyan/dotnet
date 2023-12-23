using System.Security.Cryptography.X509Certificates;

public class TaskCancelationProgram
{
    public static void Main()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Task<int> t = Task.Run(() => Sum(cts.Token, 1000000000), cts.Token);

        Thread.Sleep(5000);

        // Sometime later, cancel the CancellationTokenSource to co cancel the Task
        cts.Cancel(); // This is an asynchronous request, The thask may have complated already

        try
        {
            // If the task got cancaled, Result will throw an AggregateException
            Console.WriteLine("The sum is: " + t.Result);
        }
        catch (AggregateException ex)
        {
            // Consider any OperationCanceledException objects as handel.
            // Any other exception cause a new AggregateException containing
            // only the unhandled exception to be thrown
            ex.Handle(e => e is OperationCanceledException);

            // If all the exceptions were ahndled, the following executes.
            Console.WriteLine("Sum was cancaled");
        }
    }

    private static int Sum(CancellationToken ct, int n)
    {
        int sum = 0;
        for (; n > 0; n--)
        {
            // The folowing line throw OperationCanceldException when Cancel
            // is called on the CancalationTokenSource referred to by the token
            ct.ThrowIfCancellationRequested();

            checked
            {
                sum += n; // if n is large, this will throw System.OverflowException
            }
        }

        return sum;
    }
    //rest
}