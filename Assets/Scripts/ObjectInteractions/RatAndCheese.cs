using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAndCheese : MonoBehaviour
{
    [SerializeField] private Animator paperAnimator;
    [SerializeField] private GameObject fullCheese, eatenCheese;
    [SerializeField] 
    private ParticleSystem Electricity;
    [SerializeField] private Transform newParent;

    private void FinishRat()
    {
        GetComponent<MeshCollider>().enabled = false;
    }
    private void ReEnableCol()
    {
        GetComponent<MeshCollider>().enabled = true;
    }
    private void Flip()
    {
        paperAnimator?.SetTrigger("Flip");
    }

    public void StopParticles()
    {
        Electricity.Stop();
    }

    private void ChangeCheeses()
    {
        fullCheese.SetActive(false);
        eatenCheese.SetActive(true);
        transform.parent = newParent;
    }
}
