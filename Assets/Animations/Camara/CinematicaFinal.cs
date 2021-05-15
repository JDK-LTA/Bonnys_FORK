using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaFinal : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;

    [SerializeField]
    private GameObject canvas;


    public void DesactivarCanvas()
    {
        canvas.SetActive(false);
        GameManager.Instance.ToggleInsideOutside();
    }
    public void ActivarMainMenu()
    {
        canvas.SetActive(true);
        MainMenu.SetActive(true);
    }
}
