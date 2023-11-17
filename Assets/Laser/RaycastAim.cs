using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastAim : MonoBehaviour
{
    [SerializeField] private int castLength;
    private Invader invader;
    public MeshRenderer invaderMeshRenderer;

    public Material explosionColor;

    [SerializeField] private int laserDamage;

    public Vector3 hitPoint;

    [SerializeField] private float explosionBloomIntensity;

     private void FixedUpdate()
    {
        GetHitPoint();
    }

    public void GetHitPoint()
    {
        RaycastHit hit;

        //Debug.DrawRay(transform.position, transform.forward * castLength, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, castLength))
        {
            invader = hit.collider.GetComponent<Invader>();
            invaderMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
            //Debug.Log("HitPoint " + hit.point);
            hitPoint = hit.point;

            if (hit.collider.gameObject.tag == "Invader")
            {
                //Debug.Log("FoeColor = " + foeMeshRenderer.material.color);

                explosionColor.color = invaderMeshRenderer.material.color * explosionBloomIntensity;

                invader.TookDamage(laserDamage);
                //Debug.Log("FoeHP = " + foeBehaviour.healthPoint);
            }
        }

        //hitPoint = hit.point;
    }
}
