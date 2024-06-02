using System.Diagnostics;

namespace DataAccessLayer.Logging;

public static class LoggingFactory {
    public static ILoggingWrapper GetLogger()
    {
        StackTrace stackTrace = new StackTrace(0, false);
        var type = stackTrace.GetFrame(1)?.GetMethod()?.DeclaringType;
        var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logging/log4net.config");
        return LoggingWrapper.CreateLogger(configpath, type.FullName);
    }

}