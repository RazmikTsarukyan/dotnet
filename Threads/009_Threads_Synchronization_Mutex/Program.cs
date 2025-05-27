class Program
{
    public static string filePath = "counter.txt";
    public static object fileLock = new object();
    public static void Main()
    {
        using (var mutex = new Mutex(false, $"GlobalFileMutex:{filePath}"))
        {
            for (int i = 0; i < 10000; i++)
            {
                mutex.WaitOne();
                try
                {
                    int counter = ReadCounter(filePath);
                    counter++;
                    WriteCounter(filePath, counter);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        Console.WriteLine("Process finished!");
        Console.ReadLine();
    }

    public static int ReadCounter(string filePath)
    {
        using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
        {
            using (var reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                return string.IsNullOrEmpty(content) ? 0 : int.Parse(content);
            }
        }
    }

    public static void WriteCounter(string filePath, int counter)
    {
        using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(counter);
            }
        }
    }
}