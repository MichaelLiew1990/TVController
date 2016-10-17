using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public enum ContentType
{
    Movie,
    Game
}

public class ButtonSelect : MonoBehaviour
{
    public string title;
    public ContentType conType;
    public RectTransform rectTrans;
    public float baseScreenVal;//屏幕适应基数=屏幕宽度*0.1
    public bool isCurrent;

    private float tweenDelay;
    private Text thisText;

    public void SetTitleName(string s)
    {
        title = s;
        thisText.text = s;
    }

    public void SetTweenDelay(float f)
    {
        tweenDelay = f;
    }

    public void TweenTo(float to)
    {
        DOTween.ToAxis(() => rectTrans.anchoredPosition, x => rectTrans.anchoredPosition = x, to, tweenDelay);
    }

    public void SetCurrent()
    {
        isCurrent = true;
        DOTween.To(x => rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x),
            baseScreenVal * 4f * 0.8f, baseScreenVal * 4f, tweenDelay);
        DOTween.To(x => rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x),
            baseScreenVal * 3f * 0.8f, baseScreenVal * 3f, tweenDelay);
    }

    public void SetNotCurrent()
    {
        isCurrent = false;
        DOTween.To(x => rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x),
            baseScreenVal * 4f, baseScreenVal * 4f * 0.8f, tweenDelay);
        DOTween.To(x => rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x),
            baseScreenVal * 3f, baseScreenVal * 3f * 0.8f, tweenDelay);
    }

    void Awake()
    {
        baseScreenVal = (float)Screen.width * 0.1f;
        
        thisText = GetComponentInChildren<Text>();
        thisText.text = title;
        rectTrans = gameObject.GetComponent<RectTransform>();
    }

    void Start()
    {

    }
}
