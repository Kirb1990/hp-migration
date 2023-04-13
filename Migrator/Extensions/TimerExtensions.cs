using System;

namespace MigrationTool.Extensions;

public static class TimerExtensions
{
    public static string TimeHumanReadable(this System.Timers.Timer timer)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(timer.Interval);
        return $"{timeSpan.Hours:D2}h:{timeSpan.Minutes:D2}m:{timeSpan.Seconds:D2}s";
    }
}