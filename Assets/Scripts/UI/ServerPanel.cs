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
            SelectServerPanel.instance.Show();
            this.Hide();
        }));

        this.brnSure.onClick.Add(new EventDelegate(() =>
        {
            //确认服务器，后场景切换，保存用户选择的服务器ID等信息
            DataManager.instance.SaveLogData();
            SceneManager.LoadScene("GameScene");
        }));
        this.Hide();
    }

    public override void Show()
    {
        base.Show();
        //根据玩家上一次选择的服务器ID 更改服务器名称
        int serverId = DataManager.instance.logData.serverId;
        ServerData serverData = DataManager.instance.serverData;
        if (serverData.serverInfoDict.ContainsKey(serverId))
        {
            ServerInfo serverInfo = serverData.serverInfoDict[serverId];
            this.labSeverName.text = serverInfo.id + "区  " + serverInfo.name;
        }
    }
}
