using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PanelSelect : PanelBase
{
    public GameObject btnPrefab;
    public float tweenDelay = 0.3f;
    public RawImage imgLeftMask;
    public RawImage imgCenterMask;
    public RawImage imgRightMask;

    private List<ButtonSelect> buttons = new List<ButtonSelect>();
    private float offset;
    private int curIndex;
    private float tweenDelayCount = 0f;

    void Awake()
    {
        float baseScreenVal = (float)Screen.width * 0.1f;
        offset = baseScreenVal * 4.1f;
    }

    void Start()
    {
        int total = 10;
        for (int i = 0; i < total; i++)
        {
            GameObject obj = Instantiate(btnPrefab) as GameObject;
            obj.transform.SetParent(gameObject.transform);
            ButtonSelect btn = obj.GetComponent<ButtonSelect>();
            btn.SetTweenDelay(tweenDelay);
            btn.SetTitleName("第" + (i + 1) + "个");
            btn.rectTrans.anchoredPosition = new Vector2(i * offset, 0f);
            btn.SetNotCurrent();
            buttons.Add(btn);
        }
        curIndex = 0;
        buttons[curIndex].SetCurrent();

        imgLeftMask.transform.SetParent(gameObject.transform);
        imgCenterMask.transform.SetParent(gameObject.transform);
        imgRightMask.transform.SetParent(gameObject.transform);
        imgLeftMask.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        tweenDelayCount += Time.deltaTime;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public override void OnBack()
    {
        Application.Quit();
    }

    public override void OnMenu()
    {
        GameManager.inst.SetPage(PageType.Setting);
    }

    public override void OnEnter()
    {
        if (tweenDelayCount < tweenDelay) return;
        GameManager.inst.panelPlay.contentName = buttons[curIndex].title;
        GameManager.inst.SetPage(PageType.Play);
    }

    public override void OnLeft()
    {
        if (tweenDelayCount < tweenDelay) return;
        MoveLeftOrRight(true);
        tweenDelayCount = 0f;
    }

    public override void OnRight()
    {
        if (tweenDelayCount < tweenDelay) return;
        MoveLeftOrRight(false);
        tweenDelayCount = 0f;
    }

    void MoveLeftOrRight(bool isLeft)
    {
        float flag = 1f;
        if (isLeft)
        {
            if (curIndex <= 0) return;
            curIndex -= 1;
        }
        else
        {
            if (curIndex >= buttons.Count - 1) return;
            flag = -1f;
            curIndex += 1;
        }

        //调整显示左右遮罩
        imgLeftMask.enabled = true;
        if (curIndex <= 0)
        {
            imgLeftMask.enabled = false;
        }
        imgRightMask.enabled = true;
        if (curIndex >= buttons.Count - 1)
        {
            imgRightMask.enabled = false;
        }

        //渐变遮罩
        TweenMaskAlpha(imgLeftMask, 0.7f);
        TweenMaskAlpha(imgCenterMask, 1.0f);
        TweenMaskAlpha(imgRightMask, 0.7f);

        for (int i = 0; i < buttons.Count; i++)
        {
            ButtonSelect btn = buttons[i];
            btn.TweenTo(btn.rectTrans.anchoredPosition.x + offset * flag);
            if (i == curIndex)
            {
                btn.SetCurrent();
            }
            else
            {
                if (btn.isCurrent)
                {
                    btn.SetNotCurrent();
                }
            }
        }
    }
    

    void TweenMaskAlpha(RawImage img, float toA)
    {
        DOTween.To(x => img.color = new Color(img.color.r, img.color.g, img.color.b, x), 0f, toA, tweenDelay * 2f);
    }
}
