using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] private float wheelSensitivity = 1000f;
    [SerializeField] private float farLimitIn = 75f, closeLimitIn = 10;
    [SerializeField] private float farLimitOut = 100f, closeLimitOut = 20f;

    private void LateUpdate()
    {
        ZoomBehaviour();
    }

    private void ZoomBehaviour()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * wheelInput * wheelSensitivity * Time.deltaTime, Space.Self);
        float farLimit = GameManager.Instance.Inside ? farLimitIn : farLimitOut;
        float closeLimit = GameManager.Instance.Inside ? closeLimitIn : closeLimitOut;

        if (transform.localPosition.z < -farLimit)
        {
            transform.localPosition = new Vector3(0, 0, -farLimit);
        }
        else if (transform.localPosition.z > -closeLimit)
        {
            transform.localPosition = new Vector3(0, 0, -closeLimit);
        }
    }
}
