using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectServerPanel : UIBase<SelectServerPanel>
{
    public Transform leftScrollViewContent; // 左侧滚动视图内容区域
    public Transform rightScrollViewContent; // 右侧滚动视图内容区域
    public UILabel lastSelectedServerLabel; // 上一次选择的服务器名称

    public UILabel curServerRangeLabel; // 当前选择的服务器区服范围

    public Vector3 leftScrollViewOriginalPos; // 左侧滚动视图初始位置
    public int leftSpan = 65; // 左侧滚动视图每个按钮间隔

    public Vector3 rightScrollViewOriginalPos; // 右侧滚动视图初始位置

    public int rowSpan = 86;// 每行元素间隔

    public int colSpan = 331;// 每列元素间隔

    private int colNum = 2; // 每行显示的元素数量

    private GameObject serverRangerButtonPrefab; // 服务器区服按钮预制体
    private GameObject serverItemPrefab; // 服务器项预制体
    public override void Init()
    {
        // 动态创建左侧按钮,每5个区服为一组
        ServerData serverData = DataManager.instance.serverData;
        int groupCount = Mathf.CeilToInt(serverData.serverInfoDict.Count / 5.0f);
        this.serverRangerButtonPrefab = Resources.Load<GameObject>("Prefabs/ServerRange");
        for (int i = 0; i < groupCount; i++)
        {
            GameObject gameObject = GameObject.Instantiate(this.serverRangerButtonPrefab);
            gameObject.transform.SetParent(this.leftScrollViewContent, false);
            gameObject.transform.localPosition = this.leftScrollViewOriginalPos + new Vector3(0, -i * this.leftSpan, 0);

            // 初始化按键信息
            ServerGroup group = gameObject.GetComponent<ServerGroup>();
            group.SetLabelString(i * 5 + 1, Mathf.Min((i + 1) * 5, serverData.serverInfoDict.Count));

        }

        // 默认显示第一个区服范围的服务器列表
        this.UpdateRightScrollView(1, Mathf.Min(5, serverData.serverInfoDict.Count));
    }

    public void UpdateRightScrollView(int begin, int end)
    {
        // 清空右侧滚动视图内容
        for (int i = this.rightScrollViewContent.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(this.rightScrollViewContent.GetChild(i).gameObject);
        }

        // 动态创建右侧服务器项
        this.serverItemPrefab = Resources.Load<GameObject>("Prefabs/ChooseServer");
        ServerData serverData = DataManager.instance.serverData;
        int itemIndex = 0; // 使用独立的索引来计算位置
        for (int i = begin; i <= end; i++)
        {
            if (serverData.serverInfoDict.ContainsKey(i))
            {
                GameObject gameObject = GameObject.Instantiate(this.serverItemPrefab);
                gameObject.transform.SetParent(this.rightScrollViewContent, false);

                // 计算行列位置
                int col = itemIndex % this.colNum; // 列索引
                int row = itemIndex / this.colNum; // 行索引
                gameObject.transform.localPosition = this.rightScrollViewOriginalPos + new Vector3(col * this.colSpan, -row * this.rowSpan, 0);
                gameObject.transform.localScale = Vector3.one;

                // 初始化服务器项信息
                ServerItem serverItem = gameObject.GetComponent<ServerItem>();
                serverItem.SetLabelString(serverData.serverInfoDict[i]);

                itemIndex++; // 递增索引
            }
        }

        // 更新当前选择的服务器范围标签
        this.curServerRangeLabel.text = string.Format("服务器 {0}—{1}区", begin, end);
    }
}
