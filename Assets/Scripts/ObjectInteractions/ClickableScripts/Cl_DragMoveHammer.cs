using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveHammer : Cl_DragMove
{
    private Cl_ClickBasic click;
    [SerializeField] private Vector3 targetRot;
    [SerializeField] private float rotSpeed = 100f;
    [SerializeField] private Transform newParent;

    private Vector3 originalRot;
    private bool rotating = false, canRotate = true;

    protected override void Start()
    {
        base.Start();
        click = GetComponent<Cl_ClickBasic>();
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
        if (GameManager.Instance.IsWoodPieceInPlace)
        {
            base.Action();

            canRotate = false;
            transform.parent = newParent;
            gameObject.layer = originalLayer;
            transform.localPosition = Vector3.zero;

            TransparenceManager.Instance.UpdateRight();
            //GameManager.Instance.ManualSimpleResetTransparences();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            animator.enabled = true;
            click.enabled = true;
            enabled = false;
            //PUT AN OUTLINE IN THE HAMMER RECEIVER
        }
        else
        {
            goBack = true;
        }
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
