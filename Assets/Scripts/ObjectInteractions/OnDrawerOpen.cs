using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrawerOpen : MonoBehaviour
{
    [SerializeField] private Cl_DragBulb cl_dragBulb;
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 posFinal;

    private void OnAnimComplete()
    {
        cl_dragBulb.isMoving = true;
    }
}
