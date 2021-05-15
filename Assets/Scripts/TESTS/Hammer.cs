using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem particulasDesaparecer;
    public void DestroyHammer()
    {
        particulasDesaparecer.Play();
        //Destroy(this.gameObject);
    }

}
