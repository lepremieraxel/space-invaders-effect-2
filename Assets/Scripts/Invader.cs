using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    private int hp;
    private int damage;
    private int score;
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
    private Rigidbody rb;
    private LevelManager levelManager;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Start()
    {
        hp = 1;
        damage = 1;
        score = 3;
        x = 0.03f; y = 0.15f; yNeg = -0.15f;
        pos = Vector3.zero;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        invadersManager = GameObject.Find("InvadersManager").GetComponent<InvadersManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
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
    public void StopMoving(bool isMoving)
    {
        if (isMoving)
        {
            x = 0f;
            y = 0f;
            yNeg = 0f;
        } else
        {
            x = 0.03f; y = 0.15f; yNeg = -0.15f;
        }
    }
    public void TookDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            levelManager.AddScore(score);
            Destroy(gameObject);
        }
    }
}
