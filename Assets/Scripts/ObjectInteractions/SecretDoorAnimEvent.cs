using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorAnimEvent : MonoBehaviour
{
    [SerializeField] private GameObject redLight, greenLight;
    [SerializeField] private Collider stairsCol;
    [SerializeField] private Animator woodAnimator;
    [SerializeField] private StairsCableOutline stairsCableOutline;

    private void CanOpenNextFloor()
    {
        CamerasManager.Instance.SetWoodAndDoorCamActive(true);
        Invoke("ChangeLights", 0.75f);
        stairsCol.enabled = true;
        woodAnimator.SetTrigger("Open");
        stairsCableOutline.ActivateOutline();
    }
    private void ChangeLights()
    {
        redLight.SetActive(false);
        greenLight.SetActive(true);
    }
}
