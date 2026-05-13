using System.Diagnostics;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask2;

/// <summary>
/// Определяет реализацию для процессора наборов чисел
/// </summary>
public sealed class NumberSetProcessor : INumberSetProcessor
{
    private static readonly List<List<int>> _sets = Enumerable.Range(1, 15)
        .Select(i => Enumerable.Range(i * 10, 10).ToList())
        .ToList();

    private readonly List<ResultEntryDto> _results = new();
    private readonly object _lockObj = new();
    private TimeSpan _executionTime;

    /// <summary>
    /// Определяет реализацию для процессора наборов чисел
    /// </summary>
    public ProcessingResultDto GetResult()
    {
        return new ProcessingResultDto
        {
            Results = _results.OrderBy(r => r.SetNumber).ToList(),
            TotalSum = _results.Sum(r => r.Sum),
            ExecutionTime = _executionTime,
            ProcessedSetsCount = _results.Count
        };
    }

    /// <summary>
    /// Запускает обработку наборов в нескольких потоках
    /// </summary>
    public void Process()
    {
        _results.Clear();
        var threads = new Thread[_sets.Count];
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < _sets.Count; ++i)
        {
            int localIndex = i;
            threads[i] = new Thread(() =>
            {
                var set = _sets[localIndex];
                int sum = set.Sum();
                int threadId = Environment.CurrentManagedThreadId;

                lock (_lockObj)
                {
                    _results.Add(
                        new ResultEntryDto
                        {
                            SetNumber = localIndex + 1,
                            Sum = sum,
                            ThreadId = threadId,
                        }
                    );
                }
            });
            threads[i].Start();
        }
        foreach (var t in threads) t.Join();
        stopwatch.Stop();
        _executionTime = stopwatch.Elapsed;
    }
}
