namespace DataAccessLayer.Logging;

public interface ILoggingWrapper {
    void Debug(string message);
    void Error(string message);
    void Fatal(string message);
    void Warn(string message);
}