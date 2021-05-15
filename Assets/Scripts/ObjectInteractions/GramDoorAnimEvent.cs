using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramDoorAnimEvent : MonoBehaviour
{
    private void FinishAnim()
    {
        CamerasManager.Instance.SetGramCamActive(false);
    }
}
