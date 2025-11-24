using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerPanel : UIBase<ServerPanel>
{
    public UIButton brnSure;
    public UIButton btnChange;

    public UILabel labSeverName;

    public override void Init()
    {
        this.btnChange.onClick.Add(new EventDelegate(() =>
        {
            //打开服务器选择面板
            this.Hide();
        }));

        this.brnSure.onClick.Add(new EventDelegate(() =>
        {
            //确认服务器，后场景切换
            SceneManager.LoadScene("GameScene");
        }));

        // 测试服务器数据读取
        ServerData serverInfo = XmlDataManage.instance.LoadData(typeof(ServerData), "ServerData.xml") as ServerData;
        this.Hide();
    }

    public override void Show()
    {
        base.Show();
        //更改服务器名称
    }
}
