using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaCamara : MonoBehaviour
{

    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject UICamera;
  

    public void CambiarCamara()
    {
        UICamera.SetActive(false);
        mainCamera.SetActive(true);
    }
   
}
