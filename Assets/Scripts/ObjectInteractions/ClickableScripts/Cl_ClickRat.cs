using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickRat : Cl_ClickBasic
{
    private Collider col;

    protected override void Start()
    {
        base.Start();
        col = GetComponent<Collider>();
    }
    
    public override void Click()
    {
        base.Click();
        if (!CheeseMouseManager.Instance.CheeseInPlace || !CheeseMouseManager.Instance.IsPaperOut)
        {
            animator.SetTrigger("Click");
            col.enabled = false;
        }
        else
        {
            animator.SetTrigger("Move");
            GameManager.Instance.MouseOut = true;
            col.enabled = false;
        }
    }
}
