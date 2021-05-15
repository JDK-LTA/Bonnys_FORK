using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paperball : MonoBehaviour
{
    [SerializeField] private GameObject paperPiece;

    private void Flip(GameObject particles)
    {
        particles = Instantiate(particles, transform.position, transform.rotation);
        particles.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
        paperPiece.SetActive(true);   
    }
    private void PaperIsOut()
    {
        CheeseMouseManager.Instance.PutPaperOut();
    }
}
