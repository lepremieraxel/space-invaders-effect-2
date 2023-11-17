using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string levelFile = "Assets/Scripts/Level.csv";
    [SerializeField] private GameObject invaderTemplate;

    private Text levelTitleText;
    private GameObject levelTitle;
    private Transform spawnPoints;
    private GameObject invadersParent;
    private Text scoreText;

    private float titleTime = 2f;
    private float cooldownSpawn = 1f;
    private int maxLittleWave = 7;
    private int minLittleWave = 3;
    private int minBigWave;
    private int maxMediumWave = 16;
    private int maxBigWave = 54;
    private int hardLevel = 5;
    private int totalEnemiesSpawned;

    private bool isPlayable = false;
    private int choosenLevel;

    private InvadersManager invadersManager;
    private SpaceShipManager spaceShipManager;

    public int currentScore;
    public int enemyAlive = 0;
    public IEnumerator waveCoroutine;
    public IEnumerator startCoroutine;
    public bool stoppedCoroutine = false;

    void Awake()
    {
        levelTitleText = GameObject.Find("LevelTitle").GetComponent<Text>();
        levelTitle = GameObject.Find("LevelTitle");
        spawnPoints = GameObject.Find("SpawnPoints").transform;
        invadersParent = GameObject.Find("Invaders");
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        invadersManager = GameObject.Find("InvadersManager").GetComponent<InvadersManager>();
        spaceShipManager = GameObject.Find("SpaceShipManager").GetComponent<SpaceShipManager>();

        if (!PlayerPrefs.HasKey("totalEnemiesSpawned"))
        {
            PlayerPrefs.SetInt("totalEnemiesSpawned", 0);
            totalEnemiesSpawned = 0;
        } else
        {
            totalEnemiesSpawned = PlayerPrefs.GetInt("totalEnemiesSpawned");
        }
    }
    void Start()
    {
        minBigWave = maxLittleWave;
    }

    void Update()
    {
        spaceShipManager.canControlShip = isPlayable;
    }

    public void ChoosenLevel(int currentLevel)
    // lis le fichier csv et récupère la ligne placé en paramètres
    {
        isPlayable = false;
        choosenLevel = currentLevel;
        string[] lines = File.ReadAllLines(levelFile);
        string[] level = lines[currentLevel].Split(';');
        startCoroutine = StartLevel(level);
        if(currentLevel <= lines.Length)
        {
            StartCoroutine(startCoroutine);
        } else
        {
            Debug.Log("End");
        }
    }

    IEnumerator StartLevel(string[] level)
    {
        levelTitle.SetActive(true);
        levelTitleText.text = level[1];
        yield return new WaitForSeconds(titleTime);
        levelTitle.SetActive(false);
        StartCoroutine(CountDown(3));
        yield return new WaitForSeconds(3);
        isPlayable = true;

        int wave = int.Parse(level[2]);
        int newEnemy = 0;
        while(newEnemy <= wave && stoppedCoroutine == false)
        {
            int randEnemy = Random.Range(minLittleWave, maxLittleWave+1);
            int randColor = Random.Range(0, invadersManager.invadersMaterials.Count);
            int randMesh = Random.Range(0, invadersManager.invadersMeshes.Count);
            int randSpeed = Random.Range(1, 3);
            int randLeft = Random.Range(1, 3);
            waveCoroutine = Wave(randEnemy, randMesh, randColor, randSpeed, randLeft);
            if (randEnemy != maxLittleWave)
            {
                StartCoroutine(waveCoroutine);
                newEnemy += randEnemy;
            } else if(choosenLevel < hardLevel)
            {
                randEnemy = Random.Range(minBigWave, maxMediumWave);
                StartCoroutine(waveCoroutine);
                newEnemy += randEnemy;
            } else if(choosenLevel >= hardLevel){
                randEnemy = Random.Range(minBigWave, maxBigWave);
                StartCoroutine(waveCoroutine);
                newEnemy += randEnemy;
            }
            yield return new WaitForSeconds(cooldownSpawn);
        }
        if(newEnemy >= wave && enemyAlive == 0)
        {
            isPlayable = false;
            spaceShipManager.spaceShip.transform.position = new Vector3(0f, 0.95f, 1.5f);
            choosenLevel++;
            ChoosenLevel(choosenLevel);
        }
    }

    public IEnumerator Wave(int nbEnnemies, int randMesh, int randColor, int randSpeed, int randLeft)
    {
        int waveEnemies = 0;
        for (int i = 0; i < nbEnnemies; i++)
        {
            GameObject currentInvaderGameObject = Instantiate(invaderTemplate, spawnPoints.GetChild(waveEnemies).position, Quaternion.Euler(-90,0,90), invadersParent.transform);
            Invader newlyCreatedInvader = currentInvaderGameObject.GetComponent<Invader>();
            newlyCreatedInvader.speed = randSpeed;
            newlyCreatedInvader.meshRenderer.material = invadersManager.invadersMaterials[randColor];
            newlyCreatedInvader.meshFilter.mesh = invadersManager.invadersMeshes[randMesh];
            newlyCreatedInvader.mustGoToLeft = randLeft == 1 ? true : false;
            waveEnemies++;
            enemyAlive++;
            totalEnemiesSpawned++;
            PlayerPrefs.SetInt("totalEnemiesSpawned", totalEnemiesSpawned);
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

    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score : " + currentScore.ToString();
    }
}
