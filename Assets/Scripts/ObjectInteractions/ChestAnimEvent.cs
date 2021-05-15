using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimEvent : MonoBehaviour
{
    private void SetFeedbackCamDeactive()
    {
        CamerasManager.Instance.SetChestFeedbackCamActive(false);
    }
}
