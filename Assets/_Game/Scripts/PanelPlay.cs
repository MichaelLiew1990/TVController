using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PanelPlay : PanelBase
{
    public Text txtContentName;
    public GameObject btnPlay;
    public GameObject btnStop;
    public GameObject btnReset;

    [HideInInspector]
    public ItemContent item;

    void Start()
    {
        btnPlay.GetComponent<Button>().onClick.AddListener(StartGame);
        btnStop.GetComponent<Button>().onClick.AddListener(StopGame);
        btnReset.GetComponent<Button>().onClick.AddListener(ResetGame);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        txtContentName.text = "播放：" + item.title;
        btnPlay.SetActive(true);
        btnStop.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        StartCoroutine(PlayStopAsync());
    }

    IEnumerator PlayStopAsync()
    {
        yield return 3;//缓3帧
        if (EventSystem.current.currentSelectedGameObject == btnPlay)
        {
            btnPlay.SetActive(false);
            btnStop.SetActive(true);
            EventSystem.current.SetSelectedGameObject(btnStop, new BaseEventData(EventSystem.current));
        }
        else if (EventSystem.current.currentSelectedGameObject == btnStop)
        {
            btnPlay.SetActive(true);
            btnStop.SetActive(false);
            EventSystem.current.SetSelectedGameObject(btnPlay, new BaseEventData(EventSystem.current));
        }
    }

    public override void OnMenu()
    {
        GameManager.inst.SetPage(PageType.Setting);
    }

    public override void OnBack()
    {
        GameManager.inst.SetPage(PageType.Select);
    }

    void StartGame()
    {
        print("StartGame");
        if (GameManager.inst.net.GetNetPlayer()) GameManager.inst.net.GetNetPlayer().CmdTVServerExec(TVCommand.StartGame, item.conType, item.title);
    }

    void StopGame()
    {
        print("StopGame");
        if (GameManager.inst.net.GetNetPlayer()) GameManager.inst.net.GetNetPlayer().CmdTVServerExec(TVCommand.StopGame, item.conType, item.title);
    }

    void ResetGame()
    {
        print("ResetGame");
        if (GameManager.inst.net.GetNetPlayer()) GameManager.inst.net.GetNetPlayer().CmdTVServerExec(TVCommand.ResetGame, item.conType, item.title);
    }
}
