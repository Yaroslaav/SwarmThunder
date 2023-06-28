public class Rand
{
    private static Random _random = new Random();
    public static int Next(int min, int max)
    {
        return _random.Next(min, max);
    }
    public static int Next(int max)
    {
        return _random.Next(0, max);
    }
}
