using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCableOutline : MonoBehaviour
{
    private EPOOutline.Outlinable ol;
    private int inverter = 1;
    [SerializeField] private float speed = 5f;
    private float aux = 0;
    private bool oscillating = false;
    [SerializeField] private float timeToStopOscillation = 5f;
    private float t = 0;

    private void Start()
    {
        ol = GetComponent<EPOOutline.Outlinable>();
    }

    public void ActivateOutline()
    {
        ol.enabled = true;
        oscillating = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateOutline();
        }
        if (oscillating)
        {
            t += Time.deltaTime;
            aux += Time.deltaTime * inverter * speed;
            if (aux >= 1 && inverter == 1)
            {
                inverter = -1;
                aux = 1;
            }
            if (aux <= 0 && inverter == -1)
            {
                inverter = 1;
                aux = 0;
            }

            ol.OutlineParameters.Color = new Color(ol.OutlineParameters.Color.r, ol.OutlineParameters.Color.g, ol.OutlineParameters.Color.b, aux);

            if (t >= timeToStopOscillation)
            {
                t = 0;
                oscillating = false;
                ol.enabled = false;
            }
        }
    }
}
