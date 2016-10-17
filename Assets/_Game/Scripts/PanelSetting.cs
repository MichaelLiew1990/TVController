using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PanelSetting : PanelBase
{

    void Start()
    {

    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnBack()
    {
        GameManager.inst.SetPrePage();
    }
}