using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEditor.PlayerSettings;
using static UnityEngine.ParticleSystem;

public class FoeBehaviour : MonoBehaviour
{
    [SerializeField] public float healthPoint;

    [SerializeField] private ParticleSystem explosionEffect;

    private Color randomColor;
    [SerializeField] private MeshRenderer foeMeshRenderer;

    private bool isExplosed = false;


    private void Awake()
    {
        healthPoint = 1f;

        foeMeshRenderer.material.color = randomColor;
    }

    void Update()
    {
        //Debug.Log("Foe HP = " + healthPoint);
        if (healthPoint <= 0 && !isExplosed)
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
                isExplosed = true;
                Destroy(gameObject);
            }
        }
    }
}
