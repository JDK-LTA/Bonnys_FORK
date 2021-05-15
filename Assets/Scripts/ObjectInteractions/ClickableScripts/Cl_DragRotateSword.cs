using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragRotateSword : Cl_DragRotate
{
    [SerializeField] private Animator skullAnim;
    [SerializeField] private SoundEffect inPositionSFX;

    protected override void Action()
    {
        base.Action();

        skullAnim.SetTrigger("Open");
        CamerasManager.Instance.SetSkullCamActive(true);

        audioSource.clip = inPositionSFX.clip;
        audioSource.volume = inPositionSFX.volume;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
