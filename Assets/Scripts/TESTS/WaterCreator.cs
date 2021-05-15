using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCreator : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material mat;
    [SerializeField] private int xQuantity = 100, zQuantity = 100;
    [SerializeField] private float startPosX = 0, startPosZ = 0;
    private int aux = 0;
    //Matrix4x4 matrix;
    List<Matrix4x4> matrices = new List<Matrix4x4>();
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(startPosX, 0, startPosZ);
        transform.rotation = Quaternion.identity;
        //matrix = transform.worldToLocalMatrix;
        for (int i = 0; i < xQuantity; i++)
        {
            for (int j = 0; j < zQuantity; j++)
            {
                Matrix4x4 mx;
                transform.position = new Vector3(i + startPosX, 0, j + startPosZ);
                mx = transform.worldToLocalMatrix;

                matrices.Add(mx);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < xQuantity; i++)
        {
            for (int j = 0; j < zQuantity; j++)
            {
                //Graphics.DrawMesh(mesh, new Vector3(i + startPosX, 0, j + startPosZ), Quaternion.identity, mat, 0);
                Graphics.DrawMeshInstanced(mesh, 0, mat, matrices);
            }
        }
    }
}
