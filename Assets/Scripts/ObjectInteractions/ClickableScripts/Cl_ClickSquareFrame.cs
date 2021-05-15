using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickSquareFrame : Cl_ClickBasic
{
    [SerializeField] protected SoundEffect fallSFX;
    private bool hitOnce = false;
    public override void Click()
    {
        clicked = true;

        if (!hitOnce)
        {
            hitOnce = true;
            animator.SetTrigger("Click");
            TryToPlayClip(clickClip);
        }
        else
        {
            animator.SetTrigger("Fall");
            GetComponent<Collider>().enabled = false;
            TryToPlayClip(fallSFX);
            canBeOutlined = false;
        }
    }


}
