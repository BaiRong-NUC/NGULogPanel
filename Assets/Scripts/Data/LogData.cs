// 玩家登录数据
public class LogData
{
    public string userName;
    public string password;
    public bool isRemember;
    public bool isAutoLogin;
    public int serverId;
}

public class LogDataManager
{
    private static LogDataManager _instance = new LogDataManager();
    public static LogDataManager instance => _instance;
    private LogDataManager()
    {
        // this.logData = (LogData)XmlDataManage.instance.LoadData(typeof(LogData), "LogData");
        this.logData = XmlDataManage.instance.LoadData(typeof(LogData), "LogData") as LogData;

    }

    public LogData logData;

    public void SaveLogData()
    {
        XmlDataManage.instance.SaveData(this.logData, "LogData");
    }
}