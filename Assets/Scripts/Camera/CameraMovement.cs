using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Transform yAxisParent;
    [SerializeField] private Transform xAxisParent;
    [SerializeField] private Transform debugCube;
    [Header("SETTINGS")]
    [SerializeField] private float floorsMidHeight = 8.8f;
    [SerializeField] private float xSensitivity = 150f;
    [SerializeField] private float ySensitivity = 150f;
    [SerializeField] private float yTopLimit = 50f, yBottomLimit = 20f;
    [SerializeField] private bool xAxisInverse = false, yAxisInverse = false;

    private bool canRotate = false;

    [SerializeField] private bool deactivateAtStart = true;

    private void Start()
    {
        GameManager.Instance.MainCameraMov = this;
        ButtonManager.Instance.CamPiv = GetComponent<CameraPivoter>();
        if (deactivateAtStart)
            gameObject.SetActive(false);
    }
    private void Update()
    {
        RClickBehaviour();
    }

    private void LateUpdate()
    {
        if (canRotate)
        {
            MouseMovementBehaviour();
        }
    }

    private void MouseMovementBehaviour()
    {
        float xInput = Input.GetAxis("Mouse X");
        float yInput = Input.GetAxis("Mouse Y");
        int xInverter = xAxisInverse ? -1 : 1;
        int yInverter = yAxisInverse ? 1 : -1;

        yAxisParent.Rotate(Vector3.up, xInverter * xSensitivity * xInput * Time.deltaTime, Space.Self);
        xAxisParent.Rotate(Vector3.right, yInverter * ySensitivity * yInput * Time.deltaTime, Space.Self);

        if (xAxisParent.localRotation.eulerAngles.x > yTopLimit && xAxisParent.localRotation.eulerAngles.x < 90)
        {
            xAxisParent.localRotation = Quaternion.Euler(new Vector3(yTopLimit, 0));
        }
        else if (xAxisParent.localRotation.eulerAngles.x < 360f - yBottomLimit && xAxisParent.localRotation.eulerAngles.x > 270)
        {
            xAxisParent.localRotation = Quaternion.Euler(new Vector3(360f - yBottomLimit, 0));
        }

        if (debugCube)
            debugCube.localRotation = Quaternion.Euler(-xAxisParent.localRotation.eulerAngles + -yAxisParent.localRotation.eulerAngles);

        Transparenting();
    }

    public void Transparenting()
    {
        float aux = yAxisParent.localRotation.eulerAngles.y;

        if (GameManager.Instance.Inside)
        {
            TransparenceManager.Instance.TransparentLeft(aux >= 30 && aux <= 150);
            TransparenceManager.Instance.TransparentRight(aux >= 210 && aux <= 330);

            TransparenceManager.Instance.TransparentBack(aux >= 0 && aux <= 60 || aux <= 360 && aux >= 300);
            TransparenceManager.Instance.TransparentBL(aux >= 0 && aux <= 150 || aux <= 360 && aux >= 300);
            TransparenceManager.Instance.TransparentBLProps(aux >= 15 && aux <= 75);
            TransparenceManager.Instance.TransparentBR(aux >= 0 && aux <= 60 || aux <= 360 && aux >= 210);
            TransparenceManager.Instance.TransparentBRProps(aux >= 285 && aux <= 345);

            TransparenceManager.Instance.TransparentFront(aux >= 120 && aux <= 240);
            TransparenceManager.Instance.TransparentFLProps(aux >= 105 && aux <= 165);
            TransparenceManager.Instance.TransparentFL(aux >= 30 && aux <= 240);
            TransparenceManager.Instance.TransparentFRProps(aux >= 195 && aux <= 255);
            TransparenceManager.Instance.TransparentFR(aux >= 120 && aux <= 330);
        }
    }

    private void RClickBehaviour()
    {
        if (Input.GetMouseButtonDown(1))
        {
            canRotate = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            canRotate = false;
        }
    }
}
