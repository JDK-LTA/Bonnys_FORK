using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCards : MonoBehaviour
{
    private void OpenPanel()
    {
        CardsManager.Instance.SetClickersActive(true, true);
    }
}
