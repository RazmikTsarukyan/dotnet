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

//We use a Monitor when synchronizing threads within the same application or process,
//but if synchronization is needed between different processes, we should use a Mutex.
//A process is an instance of a running application. Each process has its own memory space and resources.
//A thread is a smaller unit of execution within a process. All threads in the same process share memory.
