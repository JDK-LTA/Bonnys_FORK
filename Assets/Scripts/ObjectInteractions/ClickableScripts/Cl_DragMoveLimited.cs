using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMoveLimited : Clickable
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Axis axisToMoveTowards = Axis.Y_AXIS;
    [SerializeField] private bool goalIsGoingPositive = true;
    [SerializeField] private float distanceToMove;

    private bool canMove = true;
    private bool inPosition = false;

    private Vector3 mousePosDelta;
    private Vector3 lastMousePos;
    private Vector3 originalPos;

    private float distanceChecker = 0;

    public override void Click()
    {
        base.Click();
        lastMousePos = Input.mousePosition;
        GameManager.Instance.ADragIsMoving = true;
    }
    public override void UnClick()
    {
        base.UnClick();
        GameManager.Instance.ADragIsMoving = false;
    }
    protected virtual void Action()
    {
        canBeOutlined = false;
        inPosition = true;
    }
    private void Start()
    {
        originalPos = transform.localPosition;
    }
    private void Update()
    {
        if (clicked && canMove)
        {
            int inverter = goalIsGoingPositive ? 1 : -1;
            mousePosDelta = Input.mousePosition - lastMousePos;

            switch (axisToMoveTowards)
            {
                case Axis.X_AXIS:
                    transform.Translate(new Vector3(mousePosDelta.x, 0, 0) * moveSpeed * Time.deltaTime * inverter, Space.Self);
                    distanceChecker += mousePosDelta.x * moveSpeed * Time.deltaTime * inverter;
                    break;
                case Axis.Y_AXIS:
                    transform.Translate(new Vector3(0, mousePosDelta.y, 0) * moveSpeed * Time.deltaTime * inverter, Space.Self);
                    distanceChecker += mousePosDelta.y * moveSpeed * Time.deltaTime * inverter;
                    break;
                case Axis.Z_AXIS:
                    transform.Translate(new Vector3(0, 0, mousePosDelta.z) * moveSpeed * Time.deltaTime * inverter, Space.Self);
                    distanceChecker += mousePosDelta.z * moveSpeed * Time.deltaTime * inverter;
                    break;
                default:
                    transform.Translate(new Vector3(0, mousePosDelta.y, 0) * moveSpeed * Time.deltaTime * inverter, Space.Self);
                    distanceChecker += mousePosDelta.y * moveSpeed * Time.deltaTime * inverter;
                    break;
            }

            if (distanceChecker < 0)
            {
                distanceChecker = 0;
                transform.localPosition = originalPos;
            }

            ActionChecker();

            lastMousePos = Input.mousePosition;
        }
    }

    private void ActionChecker()
    {
        if (!inPosition)
        {
            if (CheckIfInPosition())
            {
                switch (axisToMoveTowards)
                {
                    case Axis.X_AXIS:
                        transform.localPosition = new Vector3(originalPos.x + distanceToMove, originalPos.y, originalPos.z);
                        break;
                    case Axis.Y_AXIS:
                        transform.localPosition = new Vector3(originalPos.x, originalPos.y + distanceToMove, originalPos.z);
                        break;
                    case Axis.Z_AXIS:
                        transform.localPosition = new Vector3(originalPos.x, originalPos.y, originalPos.z + distanceToMove);
                        break;
                    default:
                        transform.localPosition = new Vector3(originalPos.x, originalPos.y + distanceToMove, originalPos.z);
                        break;
                }
                
                canMove = false;
                Action();
            }
        }
    }

    private bool CheckIfInPosition()
    {
        return distanceToMove - distanceChecker < 0.2f;
    }
}
