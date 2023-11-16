using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeBehaviour : MonoBehaviour
{
    [SerializeField] public float healthPoint;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private float delayBeforeDeath;
    //[SerializeField] private int nmbParticles;
    //[SerializeField] private Collider foeCollider;

    private void Awake()
    {
        healthPoint = 1f;
        //Instantiate(explosionParticles, this.transform.position, Quaternion.identity);
    }

    void Update()
    {
        //Debug.Log("Foe HP = " + healthPoint);
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }

        //Debug.Log("EPS duration = " + explosionParticles.main.duration);
    }

    private void OnDestroy()
    {
        explosionParticles.Play();
    }
}
