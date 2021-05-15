using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAnimEvent : MonoBehaviour
{
    [SerializeField] private Transform newParent;

    private void ChangeParent()
    {
        transform.parent = newParent;
        TransparenceManager.Instance.UpdateFront();
        //GameManager.Instance.ManualSimpleResetTransparences();
    }
}
