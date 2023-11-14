using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string levelFile = "Assets/Level.csv";
    [SerializeField] GameObject invaderTemplate;
    private Text levelTitleText;
    private GameObject levelTitle;
    private Transform spawnPoints;
    private GameObject invadersParent;
    private float titleTime = 2f;
    private float cooldownSpawn = 1f;
    private bool isPlayable = false;
    private int choosenLevel;
    private InvadersManager invadersManager;
    private int currentScore;


    void Awake()
    {
        levelTitleText = GameObject.Find("LevelTitle").GetComponent<Text>();
        levelTitle = GameObject.Find("LevelTitle");
        spawnPoints = GameObject.Find("SpawnPoints").transform;
        invadersParent = GameObject.Find("Invaders");
        invadersManager = GameObject.Find("InvadersManager").GetComponent<InvadersManager>();
    }
    void Start()
    {
        currentScore = 0;
    }

    void Update()
    {
    }

    public void ChoosenLevel(int currentLevel)
    // lis le fichier csv et récupère la ligne placé en paramètres
    {
        choosenLevel = currentLevel;
        string[] lines = File.ReadAllLines(levelFile);
        string[] level = lines[currentLevel].Split(';');
        if(currentLevel <= lines.Length)
        {
            StartCoroutine(StartLevel(level));
        } else
        {
            Debug.Log("End");
        }
    }

    IEnumerator StartLevel(string[] level)
    {
        isPlayable = false;
        levelTitle.SetActive(true);
        levelTitleText.text = level[1];
        yield return new WaitForSeconds(titleTime);
        levelTitle.SetActive(false);
        StartCoroutine(CountDown(3));
        yield return new WaitForSeconds(3);
        isPlayable = true;

        int wave = int.Parse(level[2]);
        int newEnemy = 0;
        while(newEnemy <= wave)
        {
            int randEnemy = Random.Range(3, 8);
            int randColor = Random.Range(0, invadersManager.invadersMaterials.Count);
            int randMesh = Random.Range(0, invadersManager.invadersMeshes.Count);
            int randSpeed = Random.Range(1, 3);
            int randLeft = Random.Range(1, 3);
            if (randEnemy != 7)
            {
                StartCoroutine(Wave(randEnemy, randMesh, randColor, randSpeed, randLeft));
                newEnemy += randEnemy;
            } else if(choosenLevel < 5)
            {
                randEnemy = Random.Range(8, 16);
                StartCoroutine(Wave(randEnemy, randMesh, randColor, randSpeed, randLeft));
                newEnemy += randEnemy;
            } else if(choosenLevel >= 5){
                randEnemy = Random.Range(8, 54);
                StartCoroutine(Wave(randEnemy, randMesh, randColor, randSpeed, randLeft));
                newEnemy += randEnemy;
            }
            yield return new WaitForSeconds(cooldownSpawn);
        }
        /*if(newEnemy >= wave)
        {
            choosenLevel++;
            ChoosenLevel(choosenLevel);
        }*/

        yield return new WaitForSeconds(titleTime);
    }

    IEnumerator Wave(int nbEnnemies, int randMesh, int randColor, int randSpeed, int randLeft)
    {
        int currentSpawnedEnemies = 0;
        for(int i = 0; i < nbEnnemies; i++)
        {
            GameObject currentInvaderGameObject = Instantiate(invaderTemplate, spawnPoints.GetChild(currentSpawnedEnemies).position, Quaternion.Euler(-90,0,90), invadersParent.transform);
            Invader newlyCreatedInvader = currentInvaderGameObject.GetComponent<Invader>();
            newlyCreatedInvader.speed = randSpeed;
            newlyCreatedInvader.meshRenderer.material = invadersManager.invadersMaterials[randColor];
            newlyCreatedInvader.meshFilter.mesh = invadersManager.invadersMeshes[randMesh];
            newlyCreatedInvader.mustGoToLeft = randLeft == 1 ? true : false;
            currentSpawnedEnemies++;
        }
        yield return null;
    }

    IEnumerator CountDown(int c)
    {
        levelTitle.SetActive(true);
        for (int i = c; i >= 0; i--)
        {
            if(i != 0)
            {
                levelTitleText.text = i.ToString();
            } else
            {
                levelTitleText.text = "GO";
            }
            yield return new WaitForSeconds(1);
        }
        levelTitle.SetActive(false);
    }
}
