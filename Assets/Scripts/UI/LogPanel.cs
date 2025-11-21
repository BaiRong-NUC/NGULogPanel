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
            Debug.Log("注册按钮被点击");
            this.Hide();
        }));
        this.btnLogin.onClick.Add(new EventDelegate(() =>
        {
            // Debug.Log("登录按钮被点击");
            // 判断用户名和密码是否为正确

            // 记录数据
            LogDataManager.instance.logData.userName = this.userNameInput.value;
            LogDataManager.instance.logData.password = this.passwordInput.value;
            LogDataManager.instance.logData.isRemember = this.rememberPasswordToggle.value;
            LogDataManager.instance.logData.isAutoLogin = this.autoLoginToggle.value;
            LogDataManager.instance.SaveLogData();
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
        LogData logData = LogDataManager.instance.logData;
        this.rememberPasswordToggle.value = logData.isRemember;
        this.autoLoginToggle.value = logData.isAutoLogin;
        this.userNameInput.value = logData.userName;
        if (this.rememberPasswordToggle.value == true)
        {
            this.passwordInput.value = logData.password;
        }
        if (this.autoLoginToggle.value == true)
        {
            Debug.Log("自动登录中...");
        }
    }
}
