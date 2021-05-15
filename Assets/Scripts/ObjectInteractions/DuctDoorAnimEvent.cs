using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuctDoorAnimEvent : MonoBehaviour
{
    [SerializeField] private GameObject cheeseReceiver;

    private void ActivateReceiver()
    {
        cheeseReceiver.SetActive(true);
    }
}
