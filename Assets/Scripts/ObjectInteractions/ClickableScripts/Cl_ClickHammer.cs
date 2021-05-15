using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickHammer : Cl_ClickBasic
{
    private int hammerHits = 0;
    private bool canBeHammered = true;
    [SerializeField] private Animator boardAnim;

    public override void Click()
    {
        base.Click();

        if (canBeHammered)
        {
            hammerHits++;
            if (hammerHits <= 3)
            {
                canBeHammered = false;
                canBeOutlined = false;
                CamerasManager.Instance.SetHammerCamActive(true);
                animator.SetInteger("Hits", hammerHits);
            }
        }
    }

    private void HitBoard()
    {
        boardAnim?.SetInteger("Hits", hammerHits);
    }
    private void CanHammer()
    {
        if (hammerHits < 3)
        {
            canBeHammered = true;
            canBeOutlined = true;
        }
        else
        {
            enabled = false;
        }
    }
}
