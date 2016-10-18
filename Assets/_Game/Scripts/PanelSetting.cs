using UnityEngine.UI;
using UnityEngine.Networking;

public class PanelSetting : PanelBase
{
    public Text txtServerIP;
    public RawImage imgConnecting;
    public RawImage imgConnectSuc;
    public RawImage imgConnectFal;
    public Toggle togServerBroadcast;
    public Button btnUse245Server;


    void Start()
    {
        togServerBroadcast.onValueChanged.AddListener(TogglBroadcast);
        btnUse245Server.onClick.AddListener(Use245Seaver);
        txtServerIP.text = "";
    }

    void Use245Seaver()
    {
        GameManager.inst.net.StartServerByIP("192.168.15.245");
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.inst.net != null)
        {
            if (txtServerIP.text == "" && GameManager.inst.net.netState == NetState.Connected)
            {
                txtServerIP.text = "服务器IP: " + NetworkManager.singleton.networkAddress;
                btnUse245Server.gameObject.SetActive(false);
            }
            SetNetStateImg(GameManager.inst.net.netState);
        }
    }

    public override void OnBack()
    {
        GameManager.inst.SetPrePage();
    }

    void TogglBroadcast(bool b)
    {
        NetCommand cmd = NetCommand.BroadcastYes;
        if (!b) cmd = NetCommand.BroadcastNo;
        if (GameManager.inst.net.GetNetPlayer()) GameManager.inst.net.GetNetPlayer().CmdServerExec(cmd);
    }

    void SetNetStateImg(NetState state)
    {
        imgConnecting.enabled = false;
        imgConnectSuc.enabled = false;
        imgConnectFal.enabled = false;
        if (state == NetState.Connecting)
        {
            imgConnecting.enabled = true;
        }
        else if (state == NetState.Connected)
        {
            imgConnectSuc.enabled = true;
        }
        else if (state == NetState.Failed)
        {
            imgConnectFal.enabled = true;
        }
    }
}