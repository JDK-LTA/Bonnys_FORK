using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpinner : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float rotSpeedWhenClick = 230f;
    [SerializeField] private float tToGetMaxSpeed = 0.8f;
    [SerializeField] private float tToOriginalSpeed = 3f;

    private float actualSpeed, t;
    private bool goToMax = false, goToMin = false;

    public bool GoToMax { get => goToMax; set => goToMax = value; }
    public bool GoToMin { get => goToMin; }

    private void Start()
    {
        actualSpeed = rotationSpeed;
    }

    private void Update()
    {
        if (goToMax)
        {
            t += Time.deltaTime;
            actualSpeed = Mathf.SmoothStep(rotationSpeed, rotSpeedWhenClick, t / tToGetMaxSpeed);

            if (t>=tToGetMaxSpeed)
            {
                t = 0;
                goToMax = false;
                goToMin = true;
                actualSpeed = rotSpeedWhenClick;
            }
        }
        else if (goToMin)
        {
            t += Time.deltaTime;
            actualSpeed = Mathf.SmoothStep(rotSpeedWhenClick, rotationSpeed, t / tToOriginalSpeed);

            if (t >= tToOriginalSpeed)
            {
                t = 0;
                goToMin = false;
                actualSpeed = rotationSpeed;
            }
        }

        transform.Rotate(Vector3.forward, actualSpeed * Time.deltaTime, Space.Self);
    }
}
