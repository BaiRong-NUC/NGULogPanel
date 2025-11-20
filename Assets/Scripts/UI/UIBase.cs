using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase<T> : MonoBehaviour where T : class
{
    private static T _instance;

    public static T instance => _instance;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void Start()
    {
        this.Init();
    }

    //子类继承时必须实现初始化方法
    public abstract void Init();
}
