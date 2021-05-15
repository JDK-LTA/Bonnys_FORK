using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundEffect
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool priority;
    public bool loop;
}

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(AudioSource))]
public class Clickable : MonoBehaviour
{
    protected bool clicked = false;
    protected bool isOnPlace;

    protected Rigidbody rb;
    protected Camera cam;
    protected Animator animator;
    protected AudioSource audioSource;
    protected EPOOutline.Outlinable outline;
    protected bool canBeOutlined = true;

    public bool IsOnPlace { get => isOnPlace; set => isOnPlace = value; }
    public bool CanBeOutlined { get => canBeOutlined; set => canBeOutlined = value; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        cam = Camera.main;
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
        outline = GetComponentInParent<EPOOutline.Outlinable>();
    }
    public virtual void Click()
    {
        clicked = true;
        if (outline && outline.enabled)
            outline.enabled = false;
    }
    public virtual void UnClick()
    {
        clicked = false;
    }
}
