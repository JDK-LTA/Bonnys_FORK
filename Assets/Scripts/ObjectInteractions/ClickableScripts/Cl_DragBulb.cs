using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cl_DragBulb : Cl_DragMove
{
    [SerializeField] private Animator lampAnimator;
    [SerializeField] private Transform newParent;
    [SerializeField] private ClickableReceiver_Cheese cr;
    [SerializeField] private Cl_ClickBulb clickBulb;
    [SerializeField] private float destiny=-5.92f;
    public ClickableReceiver_Cheese Cr { set => cr = value; }
    public bool isMoving = false;
    private float t = 0;
    private float time = 2;
    

    protected override void Update()
    {
        base.Update();
        if (isMoving)
        {
            t += Time.deltaTime;
            transform.localPosition=new Vector3(Mathf.SmoothStep(-8, destiny, t / time), transform.localPosition.y, transform.localPosition.z);
            if (t >= time)
            {
                t = 0;
                isMoving = false;
                transform.localPosition = new Vector3(destiny, transform.localPosition.y, transform.localPosition.z);
            }
        }
        
    }

    protected override void Action()
    {
        base.Action();
        transform.parent = newParent;
        transform.localPosition = Vector3.zero;
        gameObject.layer = newParent.gameObject.layer;
        GetComponent<Collider>().enabled = false;
        //cr.Outline.enabled = false;
        canBeOutlined = false;
        clickBulb.enabled = true;
        lampAnimator.enabled = true;
        lampAnimator.SetTrigger("PutIn");
        StartCoroutine("WaitToDesapearSelf");

        TransparenceManager.Instance.UpdateFront();
        //GameManager.Instance.ManualSimpleResetTransparences();
        
    }

    private IEnumerator WaitToDesapearSelf()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}