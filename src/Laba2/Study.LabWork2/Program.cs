using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Feature.Task1.SubTask1;
namespace Study.LabWork2;

/// <summary>
/// обертка ради обертки лабы
/// </summary>
public static class Program
{
    /// <summary>
    /// реализация обертки
    /// </summary>
    public static void Main()
    {
        var services = new IPrimeCounter[]
        {
            new MonitorService(),
            new MutexService(),
            new SemaphoreService()
        };

        int start = 1, end = 1_000_000, threadCount = 10; //10 ядер по 1 потоку на каждое

        Console.WriteLine($"Диапазон: {start}-{end}, потоков: {threadCount}\n");

        foreach (var service in services)
        {
            var result = service.CountPrimes(start, end, threadCount);
            Console.WriteLine(result.ToShortString());
        }
    }
}
