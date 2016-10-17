using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PanelBase : MonoBehaviour
{
    public GameObject firstHightlightButton;

    public virtual void OnEnable()
    {
        if (firstHightlightButton != null)
        {
            StartCoroutine(HighlightAsync());
        }
    }

    IEnumerator HighlightAsync()
    {
        yield return 3;//缓3帧
        EventSystem.current.SetSelectedGameObject(firstHightlightButton, new BaseEventData(EventSystem.current));
    }
    /// <summary>
    /// 子类复写时需要调用一下
    /// </summary>
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnDown();
        }
        else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            OnEnter();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBack();
        }
        else if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Menu))
        {
            OnMenu();
        }
    }

    public virtual void OnLeft() { print("PanelBase:OnLeft"); }

    public virtual void OnRight() { print("PanelBase:OnRight"); }

    public virtual void OnUp() { print("PanelBase:OnUp"); }

    public virtual void OnDown() { print("PanelBase:OnDown"); }

    public virtual void OnBack() { print("PanelBase:OnBack"); }

    public virtual void OnMenu() { print("PanelBase:OnMenu"); }

    public virtual void OnEnter()
    {
        if (EventSystem.current.currentSelectedGameObject == null) Debug.LogError("当前选中的按钮为空!");
        Button btn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        Toggle tog = EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();
        if (btn != null) btn.OnSubmit(new BaseEventData(EventSystem.current));
        if (tog != null) tog.OnSubmit(new BaseEventData(EventSystem.current));
    }
}