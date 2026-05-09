using Study.LabWork2.Feature.Task1.SubTask2;
namespace Study.LabWork2;

public static class Program
{
    public static void Main()
    {
        var processor = new NumberSetProcessor();
        processor.Process();
        var result = processor.GetResult();

        Console.WriteLine($"Обработано наборов: {result.ProcessedSetsCount}");
        Console.WriteLine($"Общая сумма: {result.TotalSum}");
        Console.WriteLine($"Время: {result.ExecutionTime.TotalMilliseconds:F2} мс\n");

        foreach (var entry in result.Results)
            Console.WriteLine(entry);
    }
}
