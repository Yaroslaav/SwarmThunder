
public static class ClassDestroyerExtensions
{
    public static void DestroyReference<T>(this T instance) where T : class
    {
        instance = null;
    }
}