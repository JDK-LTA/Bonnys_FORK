using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waver : MonoBehaviour
{
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float length = 2f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float offset = 0f;

    [SerializeField] public float zeroPosInY;

    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        print(meshFilter.mesh.vertices.Length);
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;

        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            //for (int j = 0; j < 4; j++)
            //{
                vertices[i /*+ j*/].y = GetWaveHeight(transform.position.x + vertices[i].x);
            //}
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
        Physics.BakeMesh(meshFilter.mesh.GetInstanceID(), false);
    }
    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + offset);
    }
}
