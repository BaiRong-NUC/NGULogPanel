using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipPanel : UIBase<TipPanel>
{
    public UIButton BtnSure; // 确认按钮
    public UILabel LabContent; // 提示内容

    public override void Init()
    {
        BtnSure.onClick.Add(new EventDelegate(() =>
        {
            this.Hide();
        }));

        this.Hide(); // 初始化时隐藏面板
    }

    // 更改提示内容
    public void ChangeContent(string content)
    {
        LabContent.text = content;
    }
}
