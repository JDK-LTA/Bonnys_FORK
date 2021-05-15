using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GramophoneMusicManager : Singleton<GramophoneMusicManager>
{
    [SerializeField] private SoundEffect brokenShanty, fixedShanty;
    [SerializeField] private Animator grammyAnimator;
    private AudioSource audioSource;
    private bool tryFix = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (tryFix)
        {
            if (!audioSource.isPlaying || audioSource.time / audioSource.clip.length > 0.75f)
            {
                tryFix = false;
                grammyAnimator.SetBool("Fixed", true);
                PutVynil(fixedShanty);
            }
        }
    }

    private void PutVynil(SoundEffect vynil)
    {
        audioSource.clip = vynil.clip;
        audioSource.loop = vynil.loop;
        audioSource.volume = vynil.volume;

        audioSource.Play();
        grammyAnimator.SetBool("Play", true);
    }
    
    public void PutBrokenVynil()
    {
        PutVynil(brokenShanty);
    }
    public void StartFixingVynil()
    {
        tryFix = true;
        audioSource.loop = false;
    }
}
