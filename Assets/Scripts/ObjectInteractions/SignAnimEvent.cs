using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignAnimEvent : MonoBehaviour
{
    [SerializeField] private Cl_ClickJustAnimation lDoor, rDoor;
    [SerializeField] private Vector3 newPos, newRot;

    private void AnimEvent()
    {
        lDoor.enabled = true;
        rDoor.enabled = true;

        GetComponent<Animator>().enabled = false;
        transform.parent = rDoor.transform;

        StartCoroutine(Agh());
    }
    IEnumerator Agh()
    {
        yield return null;
        transform.localPosition = newPos;
        transform.localEulerAngles = newRot;
    }
}
