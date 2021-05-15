using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveWallPiece : Cl_DragMove
{
    [SerializeField] private Vector3 targetRot;
    [SerializeField] private float rotSpeed = 100f;
    [SerializeField] private Transform newParent;

    private Vector3 originalRot;
    private bool rotating = false, canRotate = true;

    protected override void Start()
    {
        base.Start();
        originalRot = transform.localEulerAngles;
    }

    public override void Click()
    {
        base.Click();
        rotating = true;
    }
    public override void UnClick()
    {
        base.UnClick();
        rotating = true;
    }
    protected override void Action()
    {
        base.Action();
        canRotate = false;
        transform.parent = newParent;
        gameObject.layer = originalLayer;
        transform.localPosition = Vector3.zero;

        TransparenceManager.Instance.UpdateRight();
        //GameManager.Instance.ManualSimpleResetTransparences();
        GameManager.Instance.IsWoodPieceInPlace = true;

        rb.constraints = RigidbodyConstraints.FreezeAll;

        animator.enabled = true;
        enabled = false;
    }
    protected override void Update()
    {
        base.Update();

        if (rotating && canRotate)
        {
            if (clicked)
            {
                if (Vector3.Angle(transform.localEulerAngles, targetRot) > 0.2f)
                    transform.localEulerAngles = Vector3.Lerp(originalRot, targetRot, Time.deltaTime * rotSpeed);
                else
                {
                    transform.localEulerAngles = targetRot;
                    rotating = false;
                }
            }
            else
            {
                if (Vector3.Angle(targetRot, transform.localEulerAngles) > 0.2f)
                    transform.localEulerAngles = Vector3.Lerp(targetRot, originalRot, Time.deltaTime * rotSpeed);
                else
                {
                    transform.localEulerAngles = originalRot;
                    rotating = false;
                }
            }
        }
    }
}
