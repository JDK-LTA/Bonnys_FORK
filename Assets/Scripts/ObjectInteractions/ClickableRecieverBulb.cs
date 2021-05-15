using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableRecieverBulb : ClickableReceiver
{
    protected override void IsOnPlace(Cl_DragMove cl)
    {
        base.IsOnPlace(cl);
        //outline.enabled = true;
    }
    protected override void IsNotOnPlace(Cl_DragMove cl)
    {
        base.IsNotOnPlace(cl);
        //outline.enabled = false;
    }
}
