using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveCheese : Cl_DragMove
{
    [SerializeField] private Transform newParent;
    [SerializeField] private ClickableReceiver_Cheese cr;

    public ClickableReceiver_Cheese Cr { set => cr = value; }

    protected override void Action()
    {
        base.Action();
        transform.parent = newParent;
        transform.localPosition = Vector3.zero;
        gameObject.layer = newParent.gameObject.layer;
        GetComponent<Collider>().enabled = false;
        //cr.Outline.enabled = false;

        CheeseMouseManager.Instance.PutCheeseInPlace();

        TransparenceManager.Instance.UpdateFront();
        //GameManager.Instance.ManualSimpleResetTransparences();
    }
}

