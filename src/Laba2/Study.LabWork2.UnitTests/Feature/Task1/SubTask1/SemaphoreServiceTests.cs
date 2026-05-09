using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Feature.Task1.SubTask1;
namespace Study.LabWork2.UnitTests.Feature.Task1.SubTask1;

/// <summary>
/// Реализация юниттестов для SemaphoreService
/// </summary>
[TestFixture]
public sealed class SemaphoreServiceTests
{
    private readonly SemaphoreService _service = new();

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    public void CountPrimes_SmallRange_ReturnsCorrectCount()
    {
        var result = _service.CountPrimes(1, 10, 2);
        Assert.That(result.PrimeCount, Is.EqualTo(4));
    }

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    public void CountPrimes_LargeRange_ReturnCorrectNum()
    {
        var result = _service.CountPrimes(1, 10000, 4);
        Assert.That(result.PrimeCount, Is.EqualTo(1229));
    }

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(6)]
    public void CountPrimes_DefferentThrTheeadsCounts_SameResult(int threadCount)
    {
        var result = _service.CountPrimes(1, 10000, threadCount);
        Assert.That(result.PrimeCount, Is.EqualTo(1229));
    }

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    public void CountPrimes_ReturnsCorrectThreadCount()
    {
        var result = _service.CountPrimes(1, 100, 4);
        Assert.That(result.ThreadCount, Is.EqualTo(4));
    }

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    public void CountPrimes_ReturnsSynchronizationType()
    {
        var result = _service.CountPrimes(1, 100, 4);
        Assert.That(result.SynchronizationType, Is.EqualTo("Semaphore"));
    }

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    public void CountPrimes_FoundPrimesMatchCount()
    {
        var result = _service.CountPrimes(1, 100, 4);
        Assert.That(result.PrimeCount, Is.EqualTo(result.FoundPrimes.Count));
    }

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    public void CountPrimes_ExecutionTimeIsPositive()
    {
        var result = _service.CountPrimes(1, 10000, 4);
        Assert.That(result.ExecutionTime > TimeSpan.Zero, Is.True);
    }

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    public void IsValid_CorrectExpected_ReturnsTrue()
    {
        var result = _service.CountPrimes(1, 10000, 4);
        Assert.That(result.IsValid(1229), Is.True);
    }

    /// <summary>
    /// Реализация юниттестов для SemaphoreService
    /// </summary>
    [Test]
    public void GetVersionName_ReturnsSemaphore()
    {
        Assert.That(_service.GetVersionName(), Is.EqualTo("Semaphore"));
    }
}
