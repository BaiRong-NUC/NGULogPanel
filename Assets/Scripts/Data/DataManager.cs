public class DataManager
{
    private static DataManager _instance = new DataManager();
    public static DataManager instance => _instance;

    public LogData logData;

    // 注册数据
    public RegisterData registerData = new RegisterData();

    // 服务器数据
    public ServerData serverData;

    private DataManager()
    {
        this.logData = XmlDataManage.instance.LoadData(typeof(LogData), "LogData.xml") as LogData;

        this.registerData = XmlDataManage.instance.LoadData(typeof(RegisterData), "RegisterData.xml") as RegisterData;

        this.serverData = XmlDataManage.instance.LoadData(typeof(ServerData), "ServerData.xml") as ServerData;
    }

    public void SaveLogData()
    {
        XmlDataManage.instance.SaveData(this.logData, "LogData.xml");
    }

    public void SaveRegisterData()
    {
        XmlDataManage.instance.SaveData(this.registerData, "RegisterData.xml");
    }
}