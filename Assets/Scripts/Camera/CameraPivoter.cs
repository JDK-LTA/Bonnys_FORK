using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivoter : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private List<Transform> pivotsInOrder = new List<Transform>();
    [SerializeField] private GameObject buttonUp, buttonDown;
    private int activePivot = 0;

    [Header("SETTINGS")]
    [SerializeField] private float timeToPivot = 0.5f;
    private float t = 0;
    private bool toPivot = false;
    private int lastPivot;

    private void Start()
    {
        if (!cameraRoot)
            cameraRoot = transform.root;

        cameraRoot.position = pivotsInOrder[activePivot].position;
        lastPivot = activePivot;
    }
    private void Update()
    {
        DebugPivotting();

        if (toPivot)
        {
            MoveToActivePivot();
        }
    }

    private void DebugPivotting()
    {
        if (GameManager.Instance.Debug)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Pivot(true);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Pivot(false);
            }
        }
    }

    private void MoveToActivePivot()
    {
        t += Time.deltaTime;
        //cameraRoot.position = Vector3.Lerp(pivotsInOrder[lastPivot].position, pivotsInOrder[activePivot].position, t / timeToPivot);
        cameraRoot.position = new Vector3(Mathf.SmoothStep(pivotsInOrder[lastPivot].position.x, pivotsInOrder[activePivot].position.x, t / timeToPivot),
            Mathf.SmoothStep(pivotsInOrder[lastPivot].position.y, pivotsInOrder[activePivot].position.y, t / timeToPivot),
            Mathf.SmoothStep(pivotsInOrder[lastPivot].position.z, pivotsInOrder[activePivot].position.z, t / timeToPivot));

        if (t >= timeToPivot)
        {
            t = 0;
            toPivot = false;
            lastPivot = activePivot;
        }
    }

    public void Pivot(bool next)
    {
        TransparenceManager.Instance.UpdateMeshesSpritesAndMats();

        activePivot = next ? activePivot + 1 : activePivot - 1;
        if (activePivot >= pivotsInOrder.Count)
            activePivot = 0;
        else if (activePivot < 0)
            activePivot = pivotsInOrder.Count - 1;

        GameManager.Instance.Floor = activePivot;
        GameManager.Instance.UpdateUpDownButtonsMats();
        toPivot = true;
    }
}
