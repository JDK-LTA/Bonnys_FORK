using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickBulb : Cl_ClickBasic
{
    [SerializeField] private GameObject lampLight;
    [SerializeField] private GameObject qDiamondCard;

    private bool lightState = false;
    
    public override void Click()
    {
        base.Click();
        LightOnOff();
    }
    
    private void LightOnOff()
    {
        if (lightState)
        {
            lightState = false;
            lampLight.SetActive(false);
            qDiamondCard.SetActive(false);
        }
        else
        {
            lightState = true;
            lampLight.SetActive(true);
            qDiamondCard.SetActive(true);
        }
    }
}