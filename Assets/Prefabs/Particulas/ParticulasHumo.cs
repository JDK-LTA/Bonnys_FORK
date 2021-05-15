using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasHumo : MonoBehaviour
{
    [SerializeField]
    private GameObject particulasHumo;
   

   public void ReproducirParticulas()
    {
        Instantiate(particulasHumo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
