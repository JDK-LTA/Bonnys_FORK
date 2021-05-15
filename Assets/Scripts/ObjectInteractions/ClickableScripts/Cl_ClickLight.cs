using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickLight : Cl_ClickBasic
{
    [SerializeField] private GameObject lightGO;

    public override void Click()
    {
        base.Click();
        lightGO.SetActive(!lightGO.activeInHierarchy);
    }
}
