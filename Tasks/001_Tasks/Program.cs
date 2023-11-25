using System.Dynamic;

public class TaskProgram
{
    public static void Main()
    {
        // Create a Task (it does not running now)
        Task<int> t = new Task<int>(n => Sum((int)n), 1000000000);

        // You can start the task sometime leter
        t.Start();

        // Optionally, you can explicitly wait for the task to complate
        t.Wait();

        // You can get the result (the Result property internally calls Wait)
        Console.WriteLine("The sum is:" + t.Result);
    }

    private static int Sum(int n)
    {
        int sum = 0;
        for (; n > 0; n--)
        {
            checked { sum += n; }
        }

        return sum;
    }
}

//Calling ThreadPool’s QueueUserWorkItem method to initiate an asynchronous compute-bound 
//operation is very simple. However, this technique has many limitations. The biggest problem is that 
//there is no built-in way for you to know when the operation has completed, and there is no way to get 
//a return value back when the operation completes. To address these limitations and more, Microsoft 
//introduced the concept of tasks, and you use them via types in the System.Threading.Tasks
//namespace.

//So, instead of calling ThreadPool’s QueueUserWorkItem method, you can do the same via tasks: 
//ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5); // Calling QueueUserWorkItem 
//new Task(ComputeBoundOp, 5).Start(); // Equivalent of above using Task 
//Task.Run(() => ComputeBoundOp(5)); // Another equivalent 