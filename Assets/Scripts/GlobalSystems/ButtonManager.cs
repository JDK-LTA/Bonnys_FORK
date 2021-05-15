using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : Singleton<ButtonManager>
{
    private CameraPivoter camPiv;

    public CameraPivoter CamPiv { set => camPiv = value; }

    public void FloorGoUp()
    {
        GameManager.Instance.Floor++;
        if (GameManager.Instance.Floor >= GameManager.Instance.FloorParents.Count)
            GameManager.Instance.Floor = 0;

        camPiv.Pivot(true);
        //CHANGE THE CAMERAPIVOTER STUFF TO HERE (JUST THE GENERAL LOGIC, DO IT THERE BUT ACTIVATE HERE)
    }
    public void FloorGoDown()
    {
        GameManager.Instance.Floor--;
        if (GameManager.Instance.Floor < 0)
            GameManager.Instance.Floor = GameManager.Instance.FloorParents.Count - 1;

        camPiv.Pivot(false);
        //CHANGE THE CAMERAPIVOTER STUFF TO HERE (JUST THE GENERAL LOGIC, DO IT THERE BUT ACTIVATE HERE)
    }
    public void ToggleSpyglass()
    {

    }
    public void NextSpyglassLense()
    {

    }
}
