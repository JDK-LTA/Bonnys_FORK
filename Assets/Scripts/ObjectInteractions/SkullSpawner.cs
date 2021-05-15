using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject chestHandle;
    [SerializeField] private Transform placeToSpawn;
    public void ActivateSkull()
    {
        Instantiate(chestHandle, placeToSpawn.position, placeToSpawn.rotation, transform);
        CamerasManager.Instance.SetSkullCamActive(false);
    }
    public void PlayParticles()
    {
        ps.Play();
    }

}
