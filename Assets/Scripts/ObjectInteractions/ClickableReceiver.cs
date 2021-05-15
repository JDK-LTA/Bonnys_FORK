using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableReceiver : MonoBehaviour
{
    [SerializeField] private string tagToCheckDraggable = "";
    protected Outline outline;

    public Outline Outline { get => outline; set => outline = value; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Cl_DragMove cl = other.GetComponent<Cl_DragMove>();
        if (cl && cl.ClickTag == tagToCheckDraggable)
        {
            IsOnPlace(cl);
        }
    }

    protected virtual void IsOnPlace(Cl_DragMove cl)
    {
        cl.IsOnPlace = true;
        cl.TriggerToBeIn = GetComponent<Collider>();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        Cl_DragMove cl = other.GetComponent<Cl_DragMove>();
        if (cl && cl.ClickTag == tagToCheckDraggable)
        {
            IsNotOnPlace(cl);
        }
    }

    protected virtual void IsNotOnPlace(Cl_DragMove cl)
    {
        cl.IsOnPlace = false;
        cl.TriggerToBeIn = null;
    }

    protected void Awake()
    {
        outline = GetComponentInParent<Outline>();
    }
}
