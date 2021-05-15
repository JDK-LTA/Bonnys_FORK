using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFloorAnimEvent : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private int floorToOpen = 0;
    public void CanGoInside()
    {
        GameManager.Instance.CanGoInside[floorToOpen] = true;
        GameManager.Instance.UpdateInOutButtonsMats();
    }
}
