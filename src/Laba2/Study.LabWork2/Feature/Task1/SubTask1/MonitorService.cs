using System.Diagnostics;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask1;

/// <summary>
/// Версия 1. Использует Monitor (lock) для синхронизации
/// </summary>
public sealed class MonitorService : IPrimeCounter
{
    /// <summary>
    /// Реализация функции подсчета простых чисел на основе loc
    /// </summary>
    public PrimeCountResultDto CountPrimes(int start, int end, int threadCount)
    {
        int total = 0;
        var lockObj = new object();
        var threads = new Thread[threadCount];
        var foundPrimes = new List<int>();
        int range = (end - start + 1) / threadCount;

        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < threadCount; i++)
        {
            int localStart = start + i * range;
            int localEnd = (i == threadCount - 1) ? end : localStart + range - 1;

            threads[i] = new Thread(() =>
            {
                int localCnt = 0;
                var localPrimes = new List<int>();

                for (int num = localStart; num <= localEnd; num++)
                {
                    if (PrimeMath.IsPrime(num))
                    {
                        localCnt++;
                        localPrimes.Add(num);
                    }
                }

                lock (lockObj)
                {
                    total += localCnt;
                    foundPrimes.AddRange(localPrimes);
                }
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
    ///<summary>
    /// получаем monitor
    ///</summary>
    public string GetVersionName() => "Monitor";
}
