using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask1;

/// <summary>
/// Версия 2. Использует Mutex для синхронизации
/// </summary>
public sealed class MutexService : IPrimeCounter
{
    public PrimeCountResultDto CountPrimes(int start, int end, int threadCount)
    {
        int total = 0;
        var mutex = new Mutex();
        var threads = new Thread[threadCount];
        var foundPrimes = new List<int>();
        int range = (end - start) / threadCount;

        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < threadCount; i++)
        {
            int localStart = start + i * range;
            int localEnd = (i == threadCount - 1) ? end : localStart + range;

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

                mutex.WaitOne();
                try
                {
                    total += localCnt;
                    foundPrimes.AddRange(localPrimes);
                }
                finally { mutex.ReleaseMutex(); }
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

    static public string GetVersionName() => "Mutex";

    //public string GetVersionName() => throw new NotImplementedException();
}
