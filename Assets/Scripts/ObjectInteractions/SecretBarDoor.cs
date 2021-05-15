using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBarDoor : MonoBehaviour
{
    [SerializeField] private Transform newDoorParent;
    private Animator animator;

    private void Start()
    {
        animator = newDoorParent.GetComponentInParent<Animator>();
    }
    public void OpenDoor()
    {
        transform.parent = newDoorParent;
        TransparenceManager.Instance.UpdateRight();
        TransparenceManager.Instance.UpdateOutRight();
        //GameManager.Instance.ManualSimpleResetTransparences();
        animator.SetTrigger("Open");
    }
}
