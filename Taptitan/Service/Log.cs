namespace Taptitan.Service;

public class Log: ILog
{
    private List<string> LogList { get; set; } = new();
    public List<string> GetLog()
    {
        return LogList;
    }
    public void InsertLog(string logContent)
    {
        LogList.Add(logContent);
    }
}