﻿using System;
using System.Diagnostics;

namespace Converter
{
    public class TimestampedConsoleTraceListener : ConsoleTraceListener
    {
        public override void WriteLine(string message)
        {
            string timestampedMessage =$"{DateTime.Now:dd.MM.yyyy HH:mm:ss} {message}";
            base.WriteLine(timestampedMessage);
        }
    }
}