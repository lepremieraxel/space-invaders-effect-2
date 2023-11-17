using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastAim : MonoBehaviour
{
    [SerializeField] private int castLength;
    private Invader invader;
    private Material invaderMaterial;

    public Material explosionMaterial;

    [SerializeField] private int laserDamage;

    public Vector3 hitPoint;

    [SerializeField] private float explosionBloomIntensity;

    public void GetHitPoint()
    {
        RaycastHit hit;

        //Debug.DrawRay(transform.position, transform.forward * castLength, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, castLength))
        {
            invader = hit.collider.GetComponent<Invader>();
            invaderMaterial = hit.collider.GetComponent<MeshRenderer>().material;

            hitPoint = hit.point;

            //Debug.Log("HitPoint " + hit.point);

            if (hit.collider.gameObject.tag == "Invader")
            {
                Debug.Log("InvaderColor = " + invaderMaterial);
                explosionMaterial = invaderMaterial;
                Debug.Log("ExplosionColor = " + explosionMaterial);

                invader.TookDamage(laserDamage);
                //Debug.Log("FoeHP = " + foeBehaviour.healthPoint);
            }
        }

        //hitPoint = hit.point;
    }
}
