using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_ClickBasic : Clickable
{
    [SerializeField] protected SoundEffect clickClip;

    protected virtual void Start()
    {
        if (clickClip.clip && !audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    public override void Click()
    {
        base.Click();
        TryToPlayClip(clickClip);
    }

    protected void PlayClip(bool loop = false)
    {
        audioSource.clip = clickClip.clip;
        audioSource.volume = clickClip.volume;
        audioSource.loop = loop;
        audioSource.Play();
    }
    protected void TryToPlayClip(SoundEffect sfx)
    {
        if (audioSource && sfx.clip)
        {
            if (!sfx.priority)
            {
                if (!audioSource.isPlaying)
                {
                    PlayClip(sfx.loop);
                }
            }
            else
                PlayClip(sfx.loop);
        }
    }
}
