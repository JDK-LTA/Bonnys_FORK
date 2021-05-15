using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveRudderPiece : Cl_DragMove
{
    [SerializeField] private Transform rudder;

    private Cl_DragRotateRudder dragRotateRudder;

    protected override void Start()
    {
        base.Start();
        dragRotateRudder = FindObjectOfType<Cl_DragRotateRudder>();
    }
    public override void Click()
    {
        transform.parent = TransparenceManager.Instance.FloorMidPropsParent;
        TransparenceManager.Instance.UpdateLeft();
        //GameManager.Instance.ManualSimpleResetTransparences();
        base.Click();
    }
    protected override void BackInPlace()
    {
        base.BackInPlace();
        transform.parent = originalParent;
        TransparenceManager.Instance.UpdateLeft();
        //GameManager.Instance.ManualSimpleResetTransparences();
    }

    protected override void Action()
    {
        base.Action();
        transform.parent = rudder;
        gameObject.layer = rudder.gameObject.layer;

        TransparenceManager.Instance.UpdateRight();
        //GameManager.Instance.ManualSimpleResetTransparences();

        GetComponent<Collider>().enabled = false;
        animator.enabled = true;
        animator.SetTrigger("Screw");
        CamerasManager.Instance.SetRudderCamActive(true);
    }
    private void EnableRudderRotation()
    {
        animator.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        dragRotateRudder.enabled = true;
        CamerasManager.Instance.SetRudderCamActive(false);
    }
}
