using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Axis { X_AXIS, Y_AXIS, Z_AXIS }
public class Cl_DragRotate : Clickable
{
    [SerializeField] protected float rotationSpeed = 100f;

    [SerializeField] private bool canRotate = true;
    [SerializeField] private Axis axisToRotateAround = Axis.Z_AXIS;
    [Tooltip("This number is additive, not absolute")]
    [SerializeField] protected float targetAngleToRotate = 180;
    [SerializeField] private bool goalIsClockwise = true;
    [SerializeField] private bool canGoBack = false;

    private Vector3 lastMousePos;
    protected private Vector3 mousePosDelta;
    protected Vector3 originalRotation;

    private bool inPosition = false;
    protected float angleChecker = 0;

    public override void Click()
    {
        base.Click();
        rb.isKinematic = false;
        lastMousePos = Input.mousePosition;
    }
    public override void UnClick()
    {
        base.UnClick();
        rb.isKinematic = true;
        ActionChecker();
    }
    protected virtual void Action()
    {
        inPosition = true;
        canBeOutlined = false;
    }

    private void Start()
    {
        originalRotation = transform.localRotation.eulerAngles;
    }
    private void Update()
    {
        if (clicked && canRotate)
        {
            int inverter = goalIsClockwise ? 1 : -1;
            mousePosDelta = Input.mousePosition - lastMousePos;

            switch (axisToRotateAround)
            {
                case Axis.X_AXIS:
                    transform.Rotate(transform.right, Vector3.Dot(mousePosDelta, cam.transform.up) * rotationSpeed * Time.deltaTime * inverter, Space.World);
                    angleChecker += Vector3.Dot(mousePosDelta, cam.transform.up) * rotationSpeed * Time.deltaTime * inverter;
                    break;
                case Axis.Y_AXIS:
                    transform.Rotate(transform.up, Vector3.Dot(mousePosDelta, cam.transform.right) * rotationSpeed * Time.deltaTime * inverter, Space.World);
                    angleChecker += Vector3.Dot(mousePosDelta, cam.transform.right) * rotationSpeed * Time.deltaTime * inverter;
                    break;
                case Axis.Z_AXIS:
                    ZAxysRotation(inverter);
                    angleChecker += Vector3.Dot(mousePosDelta, cam.transform.right) * rotationSpeed * Time.deltaTime * inverter;
                    break;
                default:
                    transform.Rotate(transform.forward, Vector3.Dot(mousePosDelta, cam.transform.right) * rotationSpeed * Time.deltaTime * inverter, Space.World);
                    angleChecker += Vector3.Dot(mousePosDelta, cam.transform.right) * rotationSpeed * Time.deltaTime * inverter;
                    break;
            }

            if (angleChecker < 0)
            {
                angleChecker = 0;
                transform.localRotation = Quaternion.Euler(originalRotation);
            }

            ActionChecker();

            lastMousePos = Input.mousePosition;
        }
    }

    protected virtual void ZAxysRotation(int inverter)
    {
        transform.Rotate(transform.forward, Vector3.Dot(mousePosDelta, cam.transform.right) * rotationSpeed * Time.deltaTime * inverter, Space.World);
    }

    protected virtual void ActionChecker()
    {
        if (!inPosition)
        {
            if (CheckIfInPosition())
            {
                switch (axisToRotateAround)
                {
                    case Axis.X_AXIS:
                        transform.localRotation = Quaternion.Euler(originalRotation + new Vector3(targetAngleToRotate, 0, 0));
                        break;
                    case Axis.Y_AXIS:
                        transform.localRotation = Quaternion.Euler(originalRotation + new Vector3(0, targetAngleToRotate, 0));
                        break;
                    case Axis.Z_AXIS:
                        transform.localRotation = Quaternion.Euler(originalRotation + new Vector3(0, 0, targetAngleToRotate));
                        break;
                    default:
                        transform.localRotation = Quaternion.Euler(originalRotation + new Vector3(0, 0, targetAngleToRotate));
                        break;
                }

                canRotate = false;

                Action();
            }
        }
    }

    protected virtual bool CheckIfInPosition()
    {
        return targetAngleToRotate - angleChecker < 5;
    }
}
