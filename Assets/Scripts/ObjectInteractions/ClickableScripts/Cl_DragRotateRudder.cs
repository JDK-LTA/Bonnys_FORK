using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragRotateRudder : Cl_DragRotate
{
    [SerializeField] private Animator doorToPush;
    [SerializeField] private SoundEffect doorOpeningSFX;
    protected override void Action()
    {
        base.Action();
        doorToPush.SetTrigger("Push");

        if (audioSource && doorOpeningSFX.clip && !audioSource.isPlaying)
        {
            audioSource.clip = doorOpeningSFX.clip;
            audioSource.volume = doorOpeningSFX.volume;

            audioSource.Play();
        }
    }
}
