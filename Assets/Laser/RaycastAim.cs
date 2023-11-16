using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastAim : MonoBehaviour
{
    [SerializeField] int castLength;
    private Invader invader;
    [SerializeField] int laserDamage;

    public Vector3 hitPoint;

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
            //Debug.Log("HitPoint " + hit.point);
            hitPoint = hit.point;

            if (hit.collider.CompareTag("Invader"))
            {
                invader = hit.collider.GetComponent<Invader>();
                invader.TookDamage(laserDamage);
            }
        }

        //hitPoint = hit.point;
    }
}
