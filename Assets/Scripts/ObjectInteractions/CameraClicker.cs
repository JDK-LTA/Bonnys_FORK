using UnityEngine;

public class CameraClicker : MonoBehaviour
{
    protected Camera targetCamera;
    protected Clickable clickedObject;
    protected EPOOutline.Outlinable outlineHovered;

    // Start is called before the first frame update
    protected void Start()
    {
        targetCamera = GetComponent<Camera>();
    }
    private void OnEnable()
    {
        clickedObject?.UnClick();
        clickedObject = null;
    }
    protected void Update()
    {
        if (!targetCamera)
            return;

        OutlineSystem();

        if (Input.GetMouseButtonDown(0))
        {
            //Check if we are hovering over Clickable, if so, select it
            clickedObject = GetClickableFromClick();

        }
        if (Input.GetMouseButtonUp(0) && clickedObject)
        {
            //Release selected Clickable if there any
            clickedObject.UnClick();
            clickedObject = null;
        }
    }

    private void OutlineSystem()
    {
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rHit;

        if (Physics.Raycast(ray, out rHit))
        {
            EPOOutline.Outlinable outL = rHit.collider.GetComponentInChildren<EPOOutline.Outlinable>();
            if (!outL) outL = rHit.collider.GetComponentInParent<EPOOutline.Outlinable>();
            ClickableReceiver clickRec = outL?.GetComponentInChildren<ClickableReceiver>();

            if (outL && !GameManager.Instance.ADragIsMoving && !clickRec || outL && GameManager.Instance.ADragIsMoving && clickRec)
            {
                cl = null;
                Clickable[] cls = outL.GetComponents<Clickable>();
                for (int i = 0; i < cls.Length; i++)
                {
                    if (cls[i].enabled)
                    {
                        cl = cls[i];
                        break;
                    }
                }
                if (!cl && cls.Length > 0) cl = cls[0];

                if (!outL.enabled && cl && cl.enabled && cl.CanBeOutlined || !outL.enabled && !cl || !outL.enabled && GameManager.Instance.ADragIsMoving && cl is Cl_DragRotateRudder)
                {
                    if (outlineHovered && outlineHovered.enabled)
                        outlineHovered.enabled = false;

                    outL.enabled = true;
                    outlineHovered = outL;
                }
            }
            else
            {
                if (outlineHovered && outlineHovered.enabled)
                    outlineHovered.enabled = false;
            }
        }
        else
        {
            if (outlineHovered && outlineHovered.enabled)
                outlineHovered.enabled = false;
        }
    }

    [SerializeField] Clickable cl;
    private void DeactivateClickedObject()
    {
        if (clickedObject)
        {
            Outline ol = clickedObject.GetComponent<Outline>();
            if (ol) ol.enabled = false;
            clickedObject = null;
        }
    }

    protected virtual Clickable GetClickableFromClick()
    {
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(ray, out hitInfo))
        {
            Clickable co = null;
            Clickable[] cos = hitInfo.collider.GetComponents<Clickable>();
            for (int i = 0; i < cos.Length; i++)
            {
                if (cos[i].enabled)
                {
                    co = cos[i];
                    break;
                }
            }

            if (co)
            {
                co.Click();
                return co;
            }
        }

        return null;
    }
}