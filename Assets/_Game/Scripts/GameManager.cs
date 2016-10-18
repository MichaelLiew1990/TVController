using UnityEngine;
using UnityEngine.UI;

public enum PageType
{
    Select,
    Play,
    Setting
}

public class GameManager : MonoBehaviour
{
    public GameObject netManagerPrefab;
    public Text txtDebug;
    public static GameManager inst = null;

    [HideInInspector]
    public ClientNetworkMgr net;

    public void AddDebugInfo(string s)
    {
        txtDebug.text = s + "\n" + txtDebug.text;
    }

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
    }

    void Start()
    {
        foreach (string s in Input.GetJoystickNames())
        {
            AddDebugInfo(s);
        }

        if (!GameObject.Find("NetworkManager"))
        {
            GameObject netObj = Instantiate(netManagerPrefab) as GameObject;
            netObj.name = "NetworkManager";
            net = netObj.GetComponent<ClientNetworkMgr>();
        }
        //
        SetPage(PageType.Select);
    }

    /////================================================================================================
    public PanelSelect panelSelect;
    public PanelSetting panelSetting;
    public PanelPlay panelPlay;
    public PageType page = PageType.Select;

    private PageType prePage;


    public void SetPrePage()
    {
        SetPage(prePage);
    }

    public void SetPage(PageType p)
    {
        //prePage只保存选择页和播放页，过滤掉设置页
        if (page != PageType.Setting) prePage = page;

        page = p;
        if (p == PageType.Play)
        {
            panelPlay.gameObject.SetActive(true);
            panelSelect.gameObject.SetActive(false);
            panelSetting.gameObject.SetActive(false);
        }
        else if (p == PageType.Select)
        {
            panelPlay.gameObject.SetActive(false);
            panelSelect.gameObject.SetActive(true);
            panelSetting.gameObject.SetActive(false);
        }
        else if (p == PageType.Setting)
        {
            panelPlay.gameObject.SetActive(false);
            panelSelect.gameObject.SetActive(false);
            panelSetting.gameObject.SetActive(true);
        }
    }

    //////////==================================================================================================
}
