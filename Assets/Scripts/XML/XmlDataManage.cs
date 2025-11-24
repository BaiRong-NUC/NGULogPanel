using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

// 自定义字典序列化类
public class SerializationDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        bool wasEmpty = reader.IsEmptyElement;
        reader.Read();

        if (wasEmpty)
            return;

        // TypeDescriptor.GetConverter 支持泛型类型的字符串转换
        var keyConverter = TypeDescriptor.GetConverter(typeof(TKey));

        while (reader.NodeType == XmlNodeType.Element)
        {
            string keyStr = reader.GetAttribute("key");
            TKey key = (TKey)keyConverter.ConvertFromString(keyStr);

            reader.ReadStartElement("Item");
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            TValue value = (TValue)valueSerializer.Deserialize(reader);
            reader.ReadEndElement();

            this.Add(key, value);
        }
        // 读到父节点的结束,将结束节点读取,避免影响之后的数据读取
        if (reader.NodeType == XmlNodeType.EndElement)
            reader.Read();
    }

    public void WriteXml(XmlWriter writer)
    {
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
        foreach (TKey key in this.Keys)
        {
            writer.WriteStartElement("Item");
            writer.WriteAttributeString("key", key.ToString());
            valueSerializer.Serialize(writer, this[key]);
            writer.WriteEndElement();
        }
    }
}

public class XmlDataManage
{
    private static XmlDataManage _instance = new XmlDataManage();
    public static XmlDataManage instance => _instance;
    private XmlDataManage() { }

    // 保存数据到 XML 文件
    public void SaveData(object data, string fileName)
    {
        //1. 得到存储路径
        string path = Application.persistentDataPath + "/" + fileName;
        Debug.Log("XML数据存储路径: " + path);
        // 2. 存储文件
        using (StreamWriter writer = new StreamWriter(path))
        {
            // 3. 创建Xml序列化器
            Type type = data.GetType();
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
            // 4. 序列化对象到文件
            xmlSerializer.Serialize(writer, data);
        }
    }

    public object LoadData(Type type, string fileName)
    {
        // 1. 判定文件是否存在
        string path = Application.persistentDataPath + "/" + fileName;
        Debug.Log("XML数据加载路径: " + path);
        if (!File.Exists(path))
        {
            path = Application.streamingAssetsPath + "/" + fileName; // 从StreamingAssets目录加载默认数据
            if (!File.Exists(path))
            {
                Debug.LogError("XML数据文件不存在,返回默认数据");
                return Activator.CreateInstance(type);
            }
        }
        // 2. 读取文件
        using (StreamReader reader = new StreamReader(path))
        {
            // 3. 创建Xml序列化器
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
            // 4. 反序列化对象
            object data = xmlSerializer.Deserialize(reader);
            return data;
        }
    }
}
