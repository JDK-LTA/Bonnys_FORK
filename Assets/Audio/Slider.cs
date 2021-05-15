using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



[RequireComponent(typeof(Slider))]

public class Slider : MonoBehaviour
{
    
    Slider slider
    {
        get { return GetComponent<Slider>(); }

    }


    
    public AudioMixer mixer;
    [SerializeField]
    private string volumeName;
    [SerializeField]
  


    public void UpdateValueOnChange(float value)
    {
        float a = Mathf.Log(value) * 20f;
        if (a < -79f) a = -80f;
        mixer.SetFloat(volumeName, a);
    }
}
