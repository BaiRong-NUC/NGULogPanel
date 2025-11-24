using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPanel : UIBase<RegisterPanel>
{
    public UIInput userNameInput;
    public UIInput passwordInput;
    public UIButton btnSure;
    public UIButton btnCancel;
    public override void Init()
    {
        this.btnCancel.onClick.Add(new EventDelegate(() =>
        {
            this.Hide();
            LogPanel.instance.Show();
        }));

        this.btnSure.onClick.Add(new EventDelegate(() =>
        {
            // 将数据写到xml中
            // 判断数据是否合理,这里只判断不为空和长度
            if (this.userNameInput.value.Length == 0 || this.passwordInput.value.Length == 0)
            {
                TipPanel.instance.Show();
                TipPanel.instance.ChangeContent("用户名或密码不能为空");
                return;
            }
            if (this.passwordInput.value.Length < 6)
            {
                TipPanel.instance.Show();
                TipPanel.instance.ChangeContent("密码长度至少6位");
                return;
            }
            if(!Register(this.userNameInput.value, this.passwordInput.value))
            {
                TipPanel.instance.Show();
                TipPanel.instance.ChangeContent("用户名已存在");
                return;
            }
            //注册成功

            this.Hide();
            LogPanel.instance.Show();
            LogPanel.instance.SetPanelValue(this.userNameInput.value, this.passwordInput.value, false, false);
        }));

        this.Hide();
    }

    // 判断注册信息是否合法
    bool Register(string userName, string password)
    {
        RegisterData registerData = DataManager.instance.registerData;
        if(registerData.registerDic.ContainsKey(userName))
        {
            // 用户名已存在
            return false;
        }
        registerData.registerDic.Add(userName, password);
        // 注册成功,清空登录数据
        DataManager.instance.ClearLogData();
        DataManager.instance.SaveRegisterData();
        return true;
    }

    override public void Show()
    {
        base.Show();
        this.userNameInput.value = "";
        this.passwordInput.value = "";
    }
}
