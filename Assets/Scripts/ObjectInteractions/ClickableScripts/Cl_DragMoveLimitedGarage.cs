using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveLimitedGarage : Cl_DragMoveLimited
{
    [SerializeField] private Transform parentAfterFinish;
    protected override void Action()
    {
        base.Action();
        transform.parent = parentAfterFinish;
        TransparenceManager.Instance.UpdateTop();
        TransparenceManager.Instance.UpdateOutBack();
        //GameManager.Instance.ManualSimpleResetTransparences();
        enabled = false;
    }
}
