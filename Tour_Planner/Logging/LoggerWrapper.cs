using System;
using System.IO;

namespace Tour_Planner.Logging;

public class LoggerWrapper : ILoggerWrapper {
    private log4net.ILog _logger;

    public static LoggerWrapper CreateLogger(string configPath, string caller) {
        if (!File.Exists(configPath)) {
            throw new ArgumentException("Does not exist.", nameof(configPath));
        }

        log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
        var logger = log4net.LogManager.GetLogger(caller); // System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        return new LoggerWrapper(logger);
    }

    private LoggerWrapper(log4net.ILog logger) {
        _logger = logger;
    }

    public void Debug(string message) {
        _logger.Debug(message);
    }

    public void Warn(string message) {
        _logger.Warn(message);
    }

    public void Error(string message) {
        _logger.Error(message);
    }

    public void Fatal(string message) {
        _logger.Fatal(message);
    }
}