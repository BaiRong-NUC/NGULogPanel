using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPanel : UIBase<LogPanel>
{
    public UIInput userNameInput;
    public UIInput passwordInput;
    public UIButton btnLogin;
    public UIButton btnRegister;
    public UIToggle rememberPasswordToggle;
    public UIToggle autoLoginToggle;
    public override void Init()
    {
        // 注册事件
        this.btnRegister.onClick.Add(new EventDelegate(() =>
        {
            RegisterPanel.instance.Show();
            this.Hide();
        }));
        this.btnLogin.onClick.Add(new EventDelegate(() =>
        {
            // 判断用户名和密码是否为正确]
            if(!CheckLogin(this.userNameInput.value, this.passwordInput.value))
            {
                TipPanel.instance.Show();
                TipPanel.instance.ChangeContent("用户名或密码错误");
                return;
            }
            // 登录成功 记录数据用于测试
            DataManager.instance.logData.userName = this.userNameInput.value;
            DataManager.instance.logData.password = this.passwordInput.value;
            DataManager.instance.logData.isRemember = this.rememberPasswordToggle.value;
            DataManager.instance.logData.isAutoLogin = this.autoLoginToggle.value;
            DataManager.instance.SaveLogData();
            this.Hide();
        }));

        this.rememberPasswordToggle.onChange.Add(new EventDelegate(() =>
        {
            if (this.rememberPasswordToggle.value == false)
            {
                this.autoLoginToggle.value = false;
            }
        }));

        this.autoLoginToggle.onChange.Add(new EventDelegate(() =>
        {
            if(this.autoLoginToggle.value == true)
            {
                this.rememberPasswordToggle.value = true;
            }
        }));

        // 初始化登录界面数据
        LogData logData = DataManager.instance.logData;
        this.SetPanelValue(logData.userName, logData.password, logData.isRemember, logData.isAutoLogin);

        if (this.autoLoginToggle.value == true)
        {
            // Debug.Log("自动登录中...");
        }
    }

    // 检测登录信息是否合法
    bool CheckLogin(string userName, string password)
    {
        RegisterData registerData = DataManager.instance.registerData;
        if(registerData.registerDic.ContainsKey(userName))
        {
            // 用户名存在,检查密码
            if(registerData.registerDic[userName] == password)
            {
                // 密码正确
                return true;
            }
        }
        return false;
    }

    // 设置面板值
    public void SetPanelValue(string userName, string password,bool isRemember, bool isAutoLogin)
    {
        this.rememberPasswordToggle.value = isRemember;
        this.autoLoginToggle.value = isAutoLogin;
        this.userNameInput.value = userName;
        if (isRemember)
        {
            this.passwordInput.value = password;
        }
        else
        {
            this.passwordInput.value = "";
        }
    }
}
