using System;
using System.Diagnostics;
using System.IO;

namespace Tour_Planner.Logging;

public static class LoggingFactory {
    public static ILoggingWrapper GetLogger()
    {
        StackTrace stackTrace = new StackTrace(1, false); //Captures 1 frame, false for not collecting information about the file
        var type = stackTrace.GetFrame(1)?.GetMethod()?.DeclaringType;
        var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logging/log4net.config");
        return LoggingWrapper.CreateLogger(configpath, type.FullName);
    }

}