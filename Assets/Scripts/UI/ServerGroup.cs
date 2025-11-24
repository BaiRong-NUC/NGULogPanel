using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 选择服务器范围按钮
public class ServerGroup : MonoBehaviour
{
    public UIButton button; // 选择服务器范围按钮
    public UILabel label; // 服务器范围标签

    // 选择的服务器ID范围
    public int begin;
    public int end;

    void Start()
    {
        this.button.onClick.Add(new EventDelegate(() =>
        {
            print("ServerGroup button clicked");
            // 更新服务器展示的内容,会先调用Setring方法设置的begin和end
            SelectServerPanel.instance.UpdateRightScrollView(this.begin, this.end);
        }));
    }

    public void SetLabelString(int begin, int end)
    {
        this.label.text = string.Format("{0} - {1}区", begin, end);
        this.begin = begin;
        this.end = end;
    }
}
