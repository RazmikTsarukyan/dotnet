using System.Threading.Channels;

public class CancellationTokenRegisterProgram
{
    public static void Main()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        cancellationTokenSource.Token.Register(() => Console.WriteLine("Canceled 1"));
        cancellationTokenSource.Token.Register(() => Console.WriteLine("Cancaled 2"));
        cancellationTokenSource.Token.Register(() => Console.WriteLine("Cancaled 3"));

        cancellationTokenSource.Cancel();

        Console.WriteLine();
        Console.WriteLine(new string('-', 30));
        Console.WriteLine();

        var cts1 = new CancellationTokenSource();
        cts1.Token.Register(() => Console.WriteLine("cts1 cancaled"));

        var cts2 = new CancellationTokenSource();
        cts2.Token.Register(() => Console.WriteLine("cts2 canceled"));

        // Create a new CancellationTokenSource that is canceld when cts1 or cts2 is canceld
        var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);
        linkedCts.Token.Register(() => Console.WriteLine("linkedCts cancaled"));

        // Cancel one of the CancellationTokenSource objects (I chose cts2)
        cts2.Cancel();

        // Display wich cancalationTokenSource objects are cancaled
        Console.WriteLine("cts1 cancaled={0}, cts2 cancaled={1}, linkedCts cancaled={2}", 
            cts1.IsCancellationRequested, 
            cts2.IsCancellationRequested, 
            linkedCts.IsCancellationRequested);
    }
}

//It is often valuable to cancel an operation after a period of time has elapsed. For example, imagine a 
//server application that starts computing some work based on a client request. But the server 
//application needs to respond to the client within 2 seconds no matter what. In some scenarios, it is 
//better to respond in a short period of time with an error or with partially computed results as opposed 
//to waiting a long time for a complete result. Fortunately, CancellationTokenSource gives you a 
//way to have it self-cancel itself after a period of time.