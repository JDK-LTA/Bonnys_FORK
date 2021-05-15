using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickBottle : Cl_ClickBasic
{
    [Range(0, 5)]
    [SerializeField] private int combinationNumber;
    [SerializeField] private ParticleSystem goodJob, badJob;
    public override void Click()
    {
        base.Click();
        animator.SetTrigger("Click");
        if (!BottlesCombinationManager.Instance.PuzzleFinished && BottlesCombinationManager.Instance.PuzzleStarted)
        {
            if (BottlesCombinationManager.Instance.CombinationChecker(combinationNumber))
            {
                //Feedback de que lo haces bien
                goodJob?.Play();
            }
            else
            {
                //Feedback de que lo haces mal
                badJob?.Play();
            }
        }
    }
}
