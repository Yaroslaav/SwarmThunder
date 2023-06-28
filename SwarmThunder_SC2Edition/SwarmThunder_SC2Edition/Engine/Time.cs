using System.Diagnostics;

public static class Time
{
    public static int totalFrames = 0;
    public static int totalMilliSeconds = 0;
    public static int totalSeconds
    {
        get => totalMilliSeconds / 1000;
    }
    public static int deltaTime { get; private set; } = 0;
    private static Stopwatch timer;

    public static void Start()
    {
        timer = new();
        timer.Start();
        deltaTime = 0;
    }

    public static void UpdateTimer()
    {
        totalFrames++;
        deltaTime = timer.Elapsed.Milliseconds;
        totalMilliSeconds += deltaTime;
        timer.Restart();
    }

}