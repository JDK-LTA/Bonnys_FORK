using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickFan : Cl_ClickBasic
{
    private FanSpinner fan;
    protected override void Start()
    {
        base.Start();
        fan = GetComponentInChildren<FanSpinner>();
    }
    public override void Click()
    {
        base.Click();
        if (!fan.GoToMax && !fan.GoToMin)
        {
            fan.GoToMax = true;
        }
    }
}
