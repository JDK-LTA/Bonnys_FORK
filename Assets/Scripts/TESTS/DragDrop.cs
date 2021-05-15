using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : Clickable
{
    //[Header("Settings")]
    //[SerializeField] private float skinWidth = 0.2f;

    //[Header("Debug")]
    //[SerializeField] private bool isCaught = false;
    //private LayerMask layerToCheck = 8;
    //private LayerMask layerToIgnore = 2;
    //private Vector3 offset = Vector3.zero;

    //[SerializeField] Collider[] checker;

    //private Vector3 mouseOffset;
    //private float zMouse;

    //private Rigidbody rb;
    //private Vector3 originalRbPos;
    //[SerializeField] private float forceAmount = 500;

    //protected override void MouseDownBehaviour()
    //{
    //    zMouse = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    //    mouseOffset = gameObject.transform.position - GetMouseWorldPos();

    //    isCaught = true;

    //}
    //protected override void MouseUpBehaviour()
    //{
    //    isCaught = false;
    //}

    ////WITH RIGIDBODY
    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    //private void FixedUpdate()
    //{
    //    if (isCaught)
    //    {
    //        rb.velocity = (originalRbPos + mouseOffset - transform.position) * forceAmount * Time.deltaTime;
    //    }
    //}


    //private void OnMouseDrag()
    //{
    //Check for collision with objects
    //if (OnMultipleCollisionCheck() == Vector3.zero)
    //{
    //    transform.position = GetMouseWorldPos() + mouseOffset;
    //}
    //else
    //{
    //    if (OnMultipleCollisionCheck().x > 0 || OnMultipleCollisionCheck().x < 0)
    //    {
    //        transform.position = new Vector3 (OnMultipleCollisionCheck().x, GetMouseWorldPos().y + mouseOffset.y, GetMouseWorldPos().z + mouseOffset.z);
    //    }
    //    if (OnMultipleCollisionCheck().y > 0 || OnMultipleCollisionCheck().y < 0)
    //    {
    //        transform.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, OnMultipleCollisionCheck().y, GetMouseWorldPos().z + mouseOffset.z);
    //    }
    //    if (OnMultipleCollisionCheck().z > 0 || OnMultipleCollisionCheck().z < 0)
    //    {
    //        transform.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, GetMouseWorldPos().y + mouseOffset.y, OnMultipleCollisionCheck().z);
    //    }
    //}
    //transform.position = (GetMouseWorldPos() - OnMultipleCollisionCheck()) + mouseOffset;

    //transform.position = GetMouseWorldPos() + mouseOffset + OnMultipleCollisionCheck();
    ////}

    //private Vector3 GetMouseWorldPos()
    //{
    //    Vector3 mousePoint = Input.mousePosition;

    //    mousePoint.z = zMouse;

    //    return Camera.main.ScreenToWorldPoint(mousePoint);
    //}


    //[SerializeField] Vector3 offsetVector;
    //private Vector3 OnMultipleCollisionCheck()            //OPCION 1 MODIFICAR LA POSICION DEL GAMEOBJECT
    //{
    //    checker = Physics.OverlapSphere(gameObject.transform.position, skinWidth);
    //    if (checker.Length > 0)
    //    {
    //        /*Vector3 */
    //        offsetVector = Vector3.zero;

    //        foreach (Collider collider in checker)
    //        {
    //            //closest point to gameobject. If gameobject is clicked it should have the offset from this point. This method should ONLY apply the longest offset from each axys.
    //            //collider.ClosestPoint(gameObject.transform.position)
    //            //DIRECTION

    //            offsetVector += gameObject.transform.position - collider.ClosestPoint(gameObject.transform.position);

    //        }

    //        return offsetVector * skinWidth;
    //    }
    //    return Vector3.zero;
    //}

    //private void OnNormalCollisionCheck()             // OPCION 2 MODIFICAR EL INPUT DEL MOUSE EN EL EJE DE LA NORMAL DEL OBJETO COLLISIONADO
    //{

    //}

    //private void Start()
    //{
    //    //gameObject.layer = layerToCheck;
    //}
    //protected override void MouseDownBehaviour()
    //{
    //    //base.MouseDownBehaviour();
    //    isCaught = true;
    //    gameObject.layer = layerToIgnore;

    //    //offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}
    //protected override void MouseUpBehaviour()
    //{
    //    //base.MouseUpBehaviour();
    //    isCaught = false;
    //    gameObject.layer = layerToCheck;
    //    Drop();
    //}



    //private void Update()
    //{
    //if (isCaught)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    //Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 5f);
    //    if (Physics.Raycast(ray, out hit, 100f/*, layerToCheck*/))
    //    {
    //        //print(hit.point);
    //        //Vector3 fromCamToPoint = hit.point - Camera.main.transform.position;
    //        //Vector3 actualPoint = fromCamToPoint - fromCamToPoint.normalized * skinWidth;

    //        //transform.position = actualPoint;

    //        //float distance = hit.distance;
    //        //float newDistance = hit.distance - skinWidth;
    //        //float xc = ray.origin.x - ((newDistance * (ray.origin.x - hit.point.x)) / distance);
    //        //float yc = ray.origin.y - ((newDistance * (ray.origin.y - hit.point.y)) / distance);
    //        //float zc = ray.origin.z - ((newDistance * (ray.origin.z - hit.point.z)) / distance);

    //        //transform.position = new Vector3(xc, yc, zc);


    //        ////////////////////////////////////////////////////////////////////////////////////////
    //        Vector3 actualPoint = hit.point + hit.normal.normalized * skinWidth;
    //        //RaycastHit[] checkHits = Physics.BoxCastAll(actualPoint, new Vector3(skinWidth, skinWidth, skinWidth), Vector3.zero);
    //        //Vector3 finalPoint = actualPoint;
    //        //foreach (RaycastHit h in checkHits)
    //        //{
    //        //    finalPoint = finalPoint + h.normal.normalized * (h.distance - skinWidth);
    //        //}
    //        Vector3 finalPoint = CheckIfOutOfBounds(actualPoint);
    //        transform.position = finalPoint;
    //        ////////////////////////////////////////////////////////////////////////////////////////
    //    }
    //}
    //}
    //private Vector3 CheckIfOutOfBounds(Vector3 point)
    //{
    //    checker = Physics.OverlapSphere(point, skinWidth);
    //    if (checker.Length > 0)
    //    {
    //        Debug.DrawRay(checker[0].ClosestPoint(point), (point - checker[0].ClosestPoint(point)).normalized * (skinWidth + 0.01f), Color.red, 5);
    //        //Vector3 nextPoint = (point - checker[0].ClosestPoint(point)).normalized * (skinWidth + 0.01f);
    //        //return nextPoint;
    //    }
    //    return point;
    //}

    //private void Drop()
    //{

    //}

}
