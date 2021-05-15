using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VynilSpinner : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 8f;

    private void Update()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
    }
}
