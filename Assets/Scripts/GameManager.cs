using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int latestScore;
    private int currentLevel;
    private LevelManager levelManager;
    private GameObject levelContainer;
    public GameObject gameOverScreen;
    private GameObject startMenu;
    private GameObject spaceShip;
    private GameObject invadersParent;
    private SpaceShipManager spaceShipManager;

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        spaceShipManager = GameObject.Find("SpaceShipManager").GetComponent<SpaceShipManager>();
        startMenu = GameObject.Find("StartMenu");
        levelContainer = GameObject.Find("Level");
        gameOverScreen = GameObject.Find("GameOverScreen");
        spaceShip = GameObject.Find("SpaceShip");
        invadersParent = GameObject.Find("Invaders");
    }

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Invader"))
        {
            StopCoroutine(levelManager.waveCoroutine);
            StopCoroutine(levelManager.startCoroutine);
            levelManager.stoppedCoroutine = true;
            DestroyAllInvaders();
            GameOver();
        }
    }
    public void DestroyAllInvaders()
    {
        int i = 0;
        GameObject[] invaders = new GameObject[invadersParent.transform.childCount];

        foreach(Transform invader in invadersParent.transform)
        {
            invaders[i] = invader.gameObject;
            i++;
        }
        foreach(GameObject invader in invaders)
        {
            Destroy(invader);
        }
    }
    public void StartGame()
    {
        Debug.Log("Start");
        gameOverScreen.SetActive(false);
        levelContainer.SetActive(true);
        startMenu.SetActive(false);
        spaceShip.SetActive(true);
        currentLevel = 1;
        levelManager.currentScore = 0;
        levelManager.stoppedCoroutine = false;
        levelManager.ChoosenLevel(currentLevel);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        latestScore = levelManager.currentScore;
        spaceShipManager.canControlShip = false;
        gameOverScreen.SetActive(true);
        levelContainer.SetActive(false);
        startMenu.SetActive(false);
        spaceShip.SetActive(false);
    }

    public void StartMenu()
    {
        startMenu.SetActive(true);
        levelContainer.SetActive(false);
        gameOverScreen.SetActive(false);
        spaceShip.SetActive(false);
    }

    IEnumerator GetHighScores()
    {
        return null;
    }
    IEnumerator SetHighScore()
    {
        return null;
    }
}
