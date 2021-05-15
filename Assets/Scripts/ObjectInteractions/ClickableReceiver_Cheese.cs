using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableReceiver_Cheese : ClickableReceiver
{
    protected override void IsOnPlace(Cl_DragMove cl)
    {
        base.IsOnPlace(cl);
        if (outline)
            outline.enabled = true;
    }
    protected override void IsNotOnPlace(Cl_DragMove cl)
    {
        base.IsNotOnPlace(cl);
        if (outline)
            outline.enabled = false;
    }
}
