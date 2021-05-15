using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveVynil : Cl_DragMove
{
    [SerializeField] private Transform newParent;

    protected override void Action()
    {
        base.Action();
        transform.parent = newParent;
        gameObject.layer = newParent.gameObject.layer;
        GetComponent<Collider>().enabled = false;
        animator.enabled = true;
        animator.SetTrigger("Play");
        TransparenceManager.Instance.UpdateLeft();
        CamerasManager.Instance.SetGramCamActive(true);
    }
    private void SpinVynil()
    {
        animator.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        GetComponent<VynilSpinner>().enabled = true;
        BottlesCombinationManager.Instance.PuzzleStarted = true;
        
        CamerasManager.Instance.SetGramCamActive(false);

        GramophoneMusicManager.Instance.PutBrokenVynil();
    }
}
