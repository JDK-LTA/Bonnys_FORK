using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveChestHandle : Cl_DragMove
{
    [SerializeField] private Collider rodCol, rodSubCol, handleCol;
    [SerializeField] private Cl_DragMoveSpecialChest handleDragger;
    [SerializeField] private int newChestLayer = 12;

    public override void Click()
    {
        transform.parent = TransparenceManager.Instance.FloorMidPropsParent;
        TransparenceManager.Instance.UpdateFront();
        //GameManager.Instance.ManualSimpleResetTransparences();
        base.Click();
    }
    protected override void BackInPlace()
    {
        base.BackInPlace();
        transform.parent = originalParent;
        TransparenceManager.Instance.UpdateFront();
        //GameManager.Instance.ManualSimpleResetTransparences();
    }
    protected override void Action()
    {
        base.Action();
        transform.parent = GameManager.Instance.ChestHandleTargetParent;
        gameObject.layer = newChestLayer;
        originalLayer = newChestLayer;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = newChestLayer;
            foreach (Transform child2 in child.transform)
            {
                child2.gameObject.layer = newChestLayer;
            }
        }

        GetComponent<Collider>().enabled = false;
        triggerToBeIn.enabled = false;
        transform.localPosition = GameManager.Instance.ChestHandleTargetPosition.localPosition;

        rodCol.enabled = handleCol.enabled = rodSubCol.enabled = true;
        handleCol.attachedRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        
        handleDragger.enabled = true;

        TransparenceManager.Instance.UpdateFront();
        //GameManager.Instance.ManualSimpleResetTransparences();
        CamerasManager.Instance.CanChestCamActivate = true;
    }
}
