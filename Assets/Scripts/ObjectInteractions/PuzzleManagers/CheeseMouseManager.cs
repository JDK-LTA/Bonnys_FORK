using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseMouseManager : Singleton<CheeseMouseManager>
{
    [SerializeField] private ParticleSystem cheeseOdour;

    private bool cheeseInPlace = false, isPaperOut = false;

    public bool CheeseInPlace { get => cheeseInPlace; }
    public bool IsPaperOut { get => isPaperOut; }

    public void PutCheeseInPlace()
    {
        cheeseInPlace = true;

        if (isPaperOut)
            cheeseOdour?.Play();
    }
    public void PutPaperOut()
    {
        isPaperOut = true;

        if (cheeseInPlace)
            cheeseOdour?.Play();
    }

}
