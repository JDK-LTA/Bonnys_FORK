using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCameraClicker : CameraClicker
{
    [SerializeField] private int chestSpecialLayer = 12;

    protected override Clickable GetClickableFromClick()
    {
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(ray, out hitInfo))
        {
            Clickable co;
            co = hitInfo.collider.GetComponent<Clickable>();
            if (co)
            {
                co.Click();
                return co;
            }
        }

        return null;
    }
}
