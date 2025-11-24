using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerItem : MonoBehaviour
{
    public UIButton button; // 选择服务器按钮
    public UILabel label; // 服务器名称
    public UISprite newServerSprite; // 新服标识
    public UISprite serverStatusSprite; // 服务器状态图标

    public ServerInfo serverInfo; // 服务器信息

    void Start()
    {
        this.button.onClick.Add(new EventDelegate(() =>
        {
            // 选择这个服务器
            // print("ServerItem button clicked");
            // 记录本次选择的服务器ID
            DataManager.instance.logData.serverId = this.serverInfo.id;
            // 保存等到进入游戏场景后在保存ID,可能用户会多次选择服务器

            // 隐藏选服面板
            SelectServerPanel.instance.Hide();

            // 显示服务器面板
            ServerPanel.instance.Show();
        }));
    }

    public void SetLabelString(ServerInfo serverInfo)
    {
        this.serverInfo = serverInfo;
        //31区    天下无双
        this.label.text = serverInfo.id + "区    " + serverInfo.name;
        this.newServerSprite.gameObject.SetActive(serverInfo.isNew);

        // 更新服务器状态
        this.UpdateServerStatus(serverInfo.status);
    }

    public void UpdateServerStatus(int status)
    {
        // 服务器状态 0-无状态 1-流畅 2-繁忙 3-火爆 4-维护 
        switch (status)
        {
            case 0:
                this.serverStatusSprite.gameObject.SetActive(false);
                break;
            case 1:
                this.serverStatusSprite.spriteName = "ui_DL_liuchang_01";
                break;
            case 2:
                this.serverStatusSprite.spriteName = "ui_DL_fanhua_01";
                break;
            case 3:
                this.serverStatusSprite.spriteName = "ui_DL_huobao_01";
                break;
            case 4:
                this.serverStatusSprite.spriteName = "ui_DL_weihu_01";
                break;
        }
    }
}
