using Study.LabWork2.Feature.Task1.SubTask2;

namespace Study.LabWork2.UnitTests.Feature.Task1.SubTask2;

/// <summary>
/// Юнит тесты для NumberSetProcessor
/// </summary>
[TestFixture]
public sealed class NumberSetProcessorTests
{
    private readonly NumberSetProcessor _processor = new();

    /// <summary>
    /// Process должен обработать все 15 наборов
    /// </summary>
    [Test]
    public void Process_ProcessesAllSets()
    {
        _processor.Process();
        var result = _processor.GetResult();
        Assert.That(result.ProcessedSetsCount, Is.EqualTo(15));
    }

    /// <summary>
    /// GetResult без Process должен вернуть пустой результат
    /// </summary>
    [Test]
    public void GetResult_WithoutProcess_ReturnsEmpty()
    {
        var processor = new NumberSetProcessor();
        var result = processor.GetResult();
        Assert.That(result.ProcessedSetsCount, Is.EqualTo(0));
    }

    /// <summary>
    /// Результаты должны быть отсортированы по номеру набора
    /// </summary>
    [Test]
    public void Process_ResultsOrderedBySetNumber()
    {
        _processor.Process();
        var result = _processor.GetResult();
        var setNumbers = result.Results.Select(r => r.SetNumber).ToList();
        Assert.That(setNumbers, Is.Ordered);
    }

    /// <summary>
    /// TotalSum должен совпадать с суммой всех Results
    /// </summary>
    [Test]
    public void Process_TotalSumMatchesResultsSum()
    {
        _processor.Process();
        var result = _processor.GetResult();
        int expected = result.Results.Sum(r => r.Sum);
        Assert.That(result.TotalSum, Is.EqualTo(expected));
    }

    /// <summary>
    /// Каждый набор должен иметь уникальный номер
    /// </summary>
    [Test]
    public void Process_SetNumbersAreUnique()
    {
        _processor.Process();
        var result = _processor.GetResult();
        var unique = result.Results.Select(r => r.SetNumber).Distinct().Count();
        Assert.That(unique, Is.EqualTo(result.ProcessedSetsCount));
    }

    /// <summary>
    /// Время выполнения должно быть положительным
    /// </summary>
    [Test]
    public void Process_ExecutionTimeIsPositive()
    {
        _processor.Process();
        var result = _processor.GetResult();
        Assert.That(result.ExecutionTime > TimeSpan.Zero, Is.True);
    }

    /// <summary>
    /// Каждый поток должен иметь валидный ThreadId
    /// </summary>
    [Test]
    public void Process_ThreadIdsAreValid()
    {
        _processor.Process();
        var result = _processor.GetResult();
        Assert.That(result.Results.All(r => r.ThreadId > 0), Is.True);
    }

    /// <summary>
    /// Повторный вызов Process должен давать тот же результат
    /// </summary>
    [Test]
    public void Process_CalledTwice_SameResult()
    {
        _processor.Process();
        var result1 = _processor.GetResult();
        _processor.Process();
        var result2 = _processor.GetResult();
        Assert.That(result1.TotalSum, Is.EqualTo(result2.TotalSum));
        Assert.That(result1.ProcessedSetsCount, Is.EqualTo(result2.ProcessedSetsCount));
    }
}
