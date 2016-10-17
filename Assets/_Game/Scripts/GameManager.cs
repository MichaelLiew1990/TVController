using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    public Button btnUse245Server;
    public Text txtInfo;
    public static GameManager inst = null;

    private ClientNetworkMgr net;
    private bool isReadyForPlay = false;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
    }

    void Start()
    {
        btnUse245Server.onClick.AddListener(Use245Seaver);
        txtInfo.text = "正在连接...";

        if (!GameObject.Find("NetworkManager"))
        {
            GameObject netObj = GameObject.Instantiate(netManagerPrefab) as GameObject;
            netObj.name = "NetworkManager";
            net = netObj.GetComponent<ClientNetworkMgr>();
        }
        else
        {
            isReadyForPlay = true;
            txtInfo.text = "";
            btnUse245Server.gameObject.SetActive(false);
        }

        //
        SetPage(PageType.Select);
    }

    void Update()
    {
        if (!isReadyForPlay)
        {
            if (net != null && net.netState == NetState.Connected)
            {
                isReadyForPlay = true;
                txtInfo.text = "";
                btnUse245Server.gameObject.SetActive(false);
            }
        }
    }

    void Use245Seaver()
    {
        net.StartServerByIP("192.168.15.245");
        txtInfo.text = "正在连接192.168.15.245...";
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
