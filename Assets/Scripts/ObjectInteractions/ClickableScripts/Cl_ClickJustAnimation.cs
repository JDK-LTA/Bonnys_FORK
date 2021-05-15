using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickJustAnimation : Cl_ClickBasic
{
    [SerializeField] private bool stopOutliningAfterOneClick = false;
    public override void Click()
    {
        base.Click();
        if (animator)
        {
            try
            {
                animator.SetTrigger("Click");
            }
            catch (System.Exception)
            {
                print("Parameter is not named 'Click'");
                throw;
            }
        }
        canBeOutlined = !stopOutliningAfterOneClick;
    }
}
