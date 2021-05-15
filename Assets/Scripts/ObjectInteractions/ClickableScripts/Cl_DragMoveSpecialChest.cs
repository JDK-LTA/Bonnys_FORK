using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveSpecialChest : Cl_DragMove
{
    [SerializeField] private Camera specialCam;
    private Animator chestAnimator;

    protected override void Start()
    {
        originalLayer = 12;
        cam = CamerasManager.Instance.ChestCam;
        chestAnimator = transform.parent.parent.GetComponent<Animator>();
    }
    protected override void Action()
    {
        base.Action();

        //OPEN THE CHEST
        CamerasManager.Instance.SetChestCamActive(false);
        CamerasManager.Instance.CanChestCamActivate = false;
        chestAnimator.SetTrigger("Open");
        CamerasManager.Instance.SetChestFeedbackCamActive(true);
    }
    public override void UnClick()
    {
        clicked = false;
        rb.velocity = Vector3.zero;

        if (isOnPlace)
        {
            Action();
        }
        else
        {
            gameObject.layer = originalLayer;
        }
    }
    protected override void MovementTowardsMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 10f, 12))
        {
            Vector3 targetPos = hitInfo.point;
            rb.velocity = (targetPos - transform.position) * draggingSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
