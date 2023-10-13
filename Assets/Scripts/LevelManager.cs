using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject invaderPrefab;
    [SerializeField] string levelFile = "Assets/Level.csv";
    private Text levelTitleText;
    private GameObject levelTitle;
    private Transform spawnPoints;
    private GameObject invadersParent;
    private float titleTime = 2f;
    private int ennemiesCount = 0;
    private int currentWave = 0;
    private bool isPlayable = false;
    private int choosenLevel;
    

    void Awake()
    {
        levelTitleText = GameObject.Find("LevelTitle").GetComponent<Text>();
        levelTitle = GameObject.Find("LevelTitle");
        spawnPoints = GameObject.Find("SpawnPoints").transform;
        invadersParent = GameObject.Find("Invaders");
    }

    void Update()
    {
        ennemiesCount = invadersParent.transform.childCount;
    }

    public void ChoosenLevel(int currentLevel)
    // lis le fichier csv et récupère la ligne placé en paramètres
    {
        choosenLevel = currentLevel;
        string[] lines = File.ReadAllLines(levelFile);
        string[] level = lines[currentLevel].Split(';');
        if(currentLevel <= level.Length)
        {
            StartCoroutine(StartLevel(level));
        } else
        {
            Debug.Log("end");
        }
    }

    IEnumerator StartLevel(string[] level)
    {
        levelTitle.SetActive(true);
        levelTitleText.text = level[1];
        yield return new WaitForSeconds(titleTime);
        levelTitle.SetActive(false);
        string[] waves = level[2].Split("|");
        StartCoroutine(Wave(int.Parse(waves[currentWave])));
        if(ennemiesCount <= 0 && currentWave < waves.Length)
        {
            currentWave++;
            StartCoroutine(Wave(int.Parse(waves[currentWave])));
        } else if(ennemiesCount <= 0 && currentWave >= waves.Length && choosenLevel < level.Length)
        {
            choosenLevel++;
            ChoosenLevel(choosenLevel);
        }
    }

    IEnumerator Wave(int nbEnnemies)
    {
        levelTitleText.text = "Wave " + currentWave.ToString();
        levelTitle.SetActive(true);
        yield return new WaitForSeconds(titleTime);
        levelTitle.SetActive(false);
        for(int i = 0; i < nbEnnemies; i++)
        {
            Instantiate(invaderPrefab, spawnPoints.GetChild(ennemiesCount).position, Quaternion.identity, invadersParent.transform);
            ennemiesCount++;
        }
        StartCoroutine(CountDown(3));
        isPlayable = true;
    }

    IEnumerator CountDown(int c)
    {
        levelTitle.SetActive(true);
        for (int i = c; i >= 0; i--)
        {
            levelTitleText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        levelTitle.SetActive(false);
    }
}
