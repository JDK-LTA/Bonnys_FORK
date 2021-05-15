using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickChest : Cl_ClickBasic
{
    [SerializeField] private GameObject clickCollider, chestLockReceiver;
    public override void Click()
    {
        base.Click();

        if (!CamerasManager.Instance.ChestCamActive)
            CamerasManager.Instance.SetChestCamActive(true);

        //ACTIVATE HANDLE DRAG HERE
        clickCollider.SetActive(true);
        chestLockReceiver.SetActive(true);
    }
}
