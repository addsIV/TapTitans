namespace Taptitan.Service;

public interface ILog
{
    List<string> GetLog();
    void InsertLog(string logContent);
}