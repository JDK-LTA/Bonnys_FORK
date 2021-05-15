using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickCards : Cl_ClickBasic
{
    public override void Click()
    {
        base.Click();
        CardsManager.Instance.SetClickersActive(false);
    }
}
