// 单个服务器信息
using System.Xml.Serialization;

public class ServerInfo
{
    [XmlAttribute]
    public int id;               // 服务器ID
    [XmlAttribute]
    public string name;          // 服务器名称
    [XmlAttribute]
    public int status;         // 服务器状态
    [XmlAttribute]
    public bool isNew;          // 是否新服
}

public class ServerData
{
    // 一个服务器ID对应一个服务器信息
    public SerializationDictionary<int, ServerInfo> serverInfoDict = new SerializationDictionary<int, ServerInfo>();
}
