using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaHud : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject mainMenuPanel;
    public GameObject UiCamera;
    public void StartGame()
    {

        //mainCamera.SetActive(true);
        mainMenuPanel.SetActive(false);
        //UiCamera.SetActive(false);
        UiCamera.GetComponent<Animator>().SetTrigger("Cinematica");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void prueba()
    {
        print("prueb");
    }


}
