using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject ui_Spyglass;
    [SerializeField] private GameObject ui_MagentLens;
    [SerializeField] private GameObject ui_CyanLens;
    [SerializeField] private GameObject ui_YellowLens;
    [SerializeField] private GameObject sprite_MagentCard;
    [SerializeField] private GameObject sprite_CyanCard;
    [SerializeField] private GameObject sprite_YellowCard;

    public enum ScreenColorMode { MAGENT, CYAN, YELLOW, NORMAL }

    public ScreenColorMode currentScreenColorMode = ScreenColorMode.NORMAL;

    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentColor();
        if (GameManager.Instance.HasSpyglass) OnSpyglassObtained();
    }

    private void OnSpyglassObtained()
    {
        ui_Spyglass.SetActive(true);
        ui_MagentLens.SetActive(true);
        ui_YellowLens.SetActive(true);
        ui_CyanLens.SetActive(true);
    }

    private void CheckCurrentColor()
    {
        if (currentScreenColorMode == ScreenColorMode.NORMAL)
        {
            sprite_MagentCard.SetActive(false);
            sprite_CyanCard.SetActive(false);
            sprite_YellowCard.SetActive(false);
        }
        else if (currentScreenColorMode == ScreenColorMode.MAGENT)
        {
            sprite_MagentCard.SetActive(true);
            sprite_CyanCard.SetActive(false);
            sprite_YellowCard.SetActive(false);
        }
        else if (currentScreenColorMode == ScreenColorMode.CYAN)
        {
            sprite_MagentCard.SetActive(false);
            sprite_CyanCard.SetActive(true);
            sprite_YellowCard.SetActive(false);
        }
        else if (currentScreenColorMode == ScreenColorMode.YELLOW)
        {
            sprite_MagentCard.SetActive(false);
            sprite_CyanCard.SetActive(false);
            sprite_YellowCard.SetActive(true);
        }
    }
}
