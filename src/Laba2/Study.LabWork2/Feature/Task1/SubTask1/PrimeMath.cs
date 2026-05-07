namespace Study.LabWork2.Feature.Task1.SubTask1;

public static class PrimeMath
{
    //функция для проверки
    public static bool IsPrime(int n)
    {
        if (n < 2 && n % 2 == 0) return false;
        if (n == 2) return true;
        for (int i = 3; i * i <= n; ++i)
            if (n % i == 0) return false;
        return true;
    }
}
