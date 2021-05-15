using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> curvePivots;
    [SerializeField] private int numOfPoints = 50;
    private List<Vector3> pivotsPositions = new List<Vector3>();

    [SerializeField] private List<Vector3> pointsPositions = new List<Vector3>();

    private void Start()
    {
        foreach (Transform cp in curvePivots)
        {
            pivotsPositions.Add(cp.position);
        }

        UpdateCurve();
    }

    // Update is called once per frame
    private void Update()
    {
        Plane plane = new Plane(new Vector3(Camera.main.transform.position.x - transform.position.x, 0, Camera.main.transform.position.z - transform.position.z), transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0;
        if (plane.Raycast(ray, out enter))
        {

        }
    }

    private void UpdateCurve()
    {
        pointsPositions = BezierCurve.DeCasteljau(numOfPoints, pivotsPositions);
    }
}
