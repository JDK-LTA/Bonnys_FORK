using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUpStairs : MonoBehaviour
{
    private void CanGoUp()
    {
        GameManager.Instance.CanBeOutside[1] = true;
        GameManager.Instance.UpdateUpDownButtonsMats();
    }
}
