using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWoodAnimEvent : MonoBehaviour
{
    [SerializeField] private Collider electricDoorCol;
    private void CanOpenFloor1()
    {
        CamerasManager.Instance.SetWoodAndDoorCamActive(false);
        electricDoorCol.enabled = true;
    }
}
