namespace Study.LabWork2.Feature.Task1.SubTask1;


///<summary>
/// реализация обертки для функции IsPrime
/// </summary>
public static class PrimeMath
{

    ///<summary>
    /// реализация обертки для функции для проверки: простое число или нет
    /// </summary>
    public static bool IsPrime(int n)
    {
        if (n < 2) return false;
        if (n == 2) return true;
        if (n % 2 == 0) return false;  // после проверки на 2
        var b = Math.Sqrt(n);
        for (int i = 3; i <= b; i += 2)
            if (n % i == 0) return false;
        return true;
    }
}
