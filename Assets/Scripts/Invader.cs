using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    private int hp;
    private int damage;
    public int speed;
    private float x;
    private float y;
    private float yNeg;
    private Vector3 pos;
    public bool mustGoToLeft;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    private InvadersManager invadersManager;
    private List<Mesh> meshesList;
    private List<Material> materialsList;

    public void Start()
    {
        hp = 1;
        damage = 1;
        x = 0.03f; y = 0.15f; yNeg = -0.15f;
        pos = Vector3.zero;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        invadersManager = GameObject.Find("InvadersManager").GetComponent<InvadersManager>();
        meshesList = invadersManager.invadersMeshes;
        materialsList = invadersManager.invadersMaterials;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mur"))
        {
            if(mustGoToLeft)
            {
                mustGoToLeft = false;
            } else
            {
                mustGoToLeft = true;
            }
        }
    }

    public void FixedUpdate()
    {
        MoveFront();
        MoveLateral();
    }

    public void MoveFront()
    {
        transform.Translate(x*speed,0,0);
    }
    public void MoveLateral()
    {
        if (mustGoToLeft)
        {
            transform.Translate(0,y*speed,0);
        }
        else
        {
            transform.Translate(0,yNeg*speed,0);
        }
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }
    public void SetColor(int color)
    {
        meshRenderer.material = materialsList[color];
    }
    public void SetMesh(int mesh)
    {
        meshFilter.mesh = meshesList[mesh];
    }
    public void SetLeft(int left)
    {
        mustGoToLeft = left == 1 ? true : false;
    }
}
