using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PanelPlay : PanelBase
{
    public Text txtContentName;
    public GameObject btnPlay;
    public GameObject btnStop;

    [HideInInspector]
    public string contentName;

    public override void OnEnable()
    {
        base.OnEnable();
        txtContentName.text = "播放：" + contentName;
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
}
