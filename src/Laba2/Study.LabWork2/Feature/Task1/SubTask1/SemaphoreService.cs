using System.Diagnostics;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask1;

/// <summary>
/// Версия 3. Использует Semaphore для синхронизации
/// </summary>
public sealed class SemaphoreService : IPrimeCounter
{
    public PrimeCountResultDto CountPrimes(int start, int end, int threadCount)
    {
        var semaphore = new Semaphore(1, 1);
        var threads = new Thread[threadCount];
        var total = 0;
        int range = (end - start) / threadCount;
        var TotalPrimes = new List<int>();

        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < threadCount; ++i)
        {
            int localStart = start + i * range;
            int localEnd = (i == threadCount - 1) ? end : localStart + range;

            threads[i] = new Thread(() =>
            {
                localCnt = 0;
                localPrimes = new List<int>();

                for (int num = localStart; num < localEnd; ++num)
                {
                    if (PrimeMath.IsPrime(num))
                    {
                        localCnt++;
                        localPrimes.Add(num);
                    }
                }
                semaphore.Wait();
                try { total += totalCnt; foundPrimes.AddRange(localPrimes); }
                finally { semaphore.Release(); }
            });
            threads[i].Start();
        }

        foreach (var t in threads) t.Join();
        stopwatch.Stop();
        return new PrimeCountResultDto
        {
            PrimeCount = total,
            ExecutionTime = stopwatch.Elapsed,
            ThreadCount = threadCount,
            SynchronizationType = GetVersionName(),
            FoundPrimes = foundPrimes
        };

    }

    static public string GetVersionName() => "Semaphore";
}
