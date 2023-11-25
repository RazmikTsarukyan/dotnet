public class VoidTaskProgram
{
    public static void Main()
    {
        Task t1 = new Task(() => SayHi());
        Task t2 = new Task((person) => SayHi(((Person)person).FirstName, ((Person)person).LastName), new Person() { FirstName = "Jhon", LastName = "Smit" });

        t1.Start();
        t2.Start();

        t1.Wait();
        t2.Wait();
    }

    private static void SayHi()
    {
        Console.WriteLine("Hi");
    }

    public static void SayHi(string firstname, string lastname)
    {
        Thread.Sleep(2000);
        Console.WriteLine($"Hi {firstname} {lastname}");
    }
}

public class Person
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}