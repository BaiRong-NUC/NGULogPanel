using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TipPanel.instance.Show();
            TipPanel.instance.ChangeContent("测试提示内容!");
        }
    }
}
