using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragRotateSwitch : Cl_ClickJustAnimation
{
    [SerializeField] private SoundEffect inPositionSFX;
    [SerializeField] private float switchNumber;
    
    

    public override void Click()
    {
        base.Click();

        GoDown();

        if (GameManager.Instance.MouseOut)
        {
            SwitchIsActive();
        }
        else StartCoroutine("WaitAndGoUp");

        //audioSource.clip = inPositionSFX.clip;
        //audioSource.volume = inPositionSFX.volume;
        //if (!audioSource.isPlaying)
        //    audioSource.Play();
    }

    private void GoUp()
    {
        animator.SetTrigger("Up");
    }

    private void GoDown()
    {
        animator.SetTrigger("Down");
    }

    private void SwitchIsActive()
    {
        if (switchNumber == 1)
        {
            GameManager.Instance.Switch1On = true;
        }
        if (switchNumber == 2)
        {
            GameManager.Instance.Switch2On = true;
        }
        if (switchNumber == 3)
        {
            GameManager.Instance.Switch3On = true;
        }
    }
    
    private IEnumerator WaitAndGoUp()
    {
        yield return new WaitForSeconds(2f);
        GoUp();
    }
}
