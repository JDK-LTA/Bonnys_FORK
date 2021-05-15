using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesCombinationManager : Singleton<BottlesCombinationManager>
{
    [SerializeField] private int[] combination = { 5, 5, 4, 2, 3, 1, 2, 0 };
    [SerializeField] private Animator grammyDoorAnim;
    
    [Header("Debug")]
    [SerializeField] private bool[] comboInput = { false, false, false, false, false, false, false, false };

    private bool puzzleFinished, puzzleStarted;
    public bool PuzzleFinished { get => puzzleFinished; }
    public bool PuzzleStarted { get => puzzleStarted; set => puzzleStarted = value; }

    public bool CombinationChecker(int input)
    {
        int place = NextPlaceToCheck();
        
        if (input == combination[place])
        {
            comboInput[place] = true;
            if (place == comboInput.Length - 1)
            {
                FinishPuzzle();
            }
            return true;
        }
        else
        {
            ResetComboInput();
            return false;
        }
    }

    private void FinishPuzzle()
    {
        //Abrir gramófono y empezar a sonar la música completa
        grammyDoorAnim.SetTrigger("Open");
        puzzleFinished = true;

        CamerasManager.Instance.SetGramCamActive(true);

        GramophoneMusicManager.Instance.StartFixingVynil();
    }

    private int NextPlaceToCheck()
    {
        for (int i = 0; i < comboInput.Length; i++)
        {
            if (!comboInput[i])
            {
                return i;
            }
        }
        return -1;
    }
    private void ResetComboInput()
    {
        for (int i = 0; i < comboInput.Length; i++)
        {
            comboInput[i] = false;
        }
    }

}
