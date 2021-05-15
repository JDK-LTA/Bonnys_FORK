using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperWall : MonoBehaviour
{
    [SerializeField] private Animator paperAnim;

    private void Paper1()
    {
        paperAnim.SetInteger("Hits", 1);
        CamerasManager.Instance.SetHammerCamActive(false);
    }
    private void Paper2()
    {
        paperAnim.SetInteger("Hits", 2);
        CamerasManager.Instance.SetHammerCamActive(false);
    }
    private void Paper3()
    {
        paperAnim.SetInteger("Hits", 3);
        CamerasManager.Instance.SetHammerCamActive(false);
    }
}
