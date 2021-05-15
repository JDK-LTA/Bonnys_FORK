using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragMove : Clickable
{
    [SerializeField] protected float draggingSpeed = 500f;
    [SerializeField] protected float goingBackSpeed = 20f;
    [SerializeField] protected string tagToCheckReciever;

    protected Vector3 originalPos;
    protected bool goBack = false;
    protected int originalLayer;
    protected Transform originalParent;

    protected Collider triggerToBeIn;

    public string ClickTag { get => tagToCheckReciever; }
    public Collider TriggerToBeIn { set => triggerToBeIn = value; }

    protected virtual void Start()
    {
        originalLayer = gameObject.layer;
        originalParent = transform.parent;
        originalPos = transform.position;
    }

    public override void Click()
    {
        base.Click();
        gameObject.layer = 2;
        originalPos = transform.position;
        rb.isKinematic = false;
        rb.useGravity = false;
        GameManager.Instance.ADragIsMoving = true;
    }
    public override void UnClick()
    {
        base.UnClick();
        rb.velocity = Vector3.zero;

        if (isOnPlace)
        {
            Action();
        }
        else
        {
            goBack = true;
        }

        GameManager.Instance.ADragIsMoving = false;
    }

    protected virtual void Action()
    {
        canBeOutlined = false;
    }

    protected virtual void FixedUpdate()
    {
        if (clicked)
        {
            MovementTowardsMouse();
        }
    }

    protected virtual void MovementTowardsMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 targetPos = hitInfo.point + hitInfo.normal.normalized;
            rb.velocity = (targetPos - transform.position) * draggingSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    protected virtual void Update()
    {
        if (goBack)
        {
            if (Vector3.Distance(originalPos, transform.position) > 0.2f)
                transform.position = Vector3.Lerp(transform.position, originalPos, Time.deltaTime * goingBackSpeed);
            else
            {
                BackInPlace();
            }
        }
    }
    protected virtual void BackInPlace()
    {
        transform.position = originalPos;
        goBack = false;
        gameObject.layer = originalLayer;
        rb.isKinematic = true;
    }
}
