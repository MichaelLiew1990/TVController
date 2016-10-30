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
    private float offset;//左右图片偏移值
    private int curIndex;
    private float tweenDelayCount = 0f;

    void Awake()
    {
        float baseScreenVal = (float)Screen.width * 0.1f;
        offset = baseScreenVal * 4.7f;
    }

    void Start()
    {
        //设置展示数据内容
        int total = 10;
        for (int i = 0; i < total; i++)
        {
            GameObject obj = Instantiate(btnPrefab) as GameObject;
            obj.transform.SetParent(gameObject.transform);
            ButtonSelect btn = obj.GetComponent<ButtonSelect>();
            btn.SetTweenDelay(tweenDelay);

            btn.item.title = "File" + (i + 1);
            btn.item.beanCost = 50;
            btn.item.conType = ContentType.GameWithPad;
            btn.item.introduction = "束带结发开始叫对方考虑，阿萨德发奖励卡加速度卡。洛手机打发了空间啊爱神，的箭法拉克加快速度就，付了款安静的弗兰克阿克苏的解放了看见卡上的副经理级！";
            btn.item.texture = new GUITexture();

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
        if (EventSystem.current) EventSystem.current.SetSelectedGameObject(null);
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
        GameManager.inst.panelPlay.item = buttons[curIndex].item;
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
        TweenMaskAlpha(imgLeftMask, 0.7f, 0.5f);
        TweenMaskAlpha(imgCenterMask, 0.85f, 2.0f);
        TweenMaskAlpha(imgRightMask, 0.7f, 0.5f);

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
    

    void TweenMaskAlpha(RawImage img, float toA, float delay)
    {
        DOTween.To(x => img.color = new Color(img.color.r, img.color.g, img.color.b, x), 0f, toA, delay);
    }
}
