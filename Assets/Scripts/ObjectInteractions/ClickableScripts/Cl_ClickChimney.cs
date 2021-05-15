using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickChimney : Cl_ClickJustAnimation
{
    [SerializeField] private ParticleSystem burstSmokePs;
    public override void Click()
    {
        base.Click();
        burstSmokePs.Play();
    }
}
