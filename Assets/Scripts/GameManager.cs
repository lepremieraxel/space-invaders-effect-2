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
    private GameObject gameOverScreen;
    private GameObject startMenu;
    private GameObject spaceShip;

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        startMenu = GameObject.Find("StartMenu");
        levelContainer = GameObject.Find("Level");
        gameOverScreen = GameObject.Find("GameOverScreen");
        spaceShip = GameObject.Find("SpaceShip");
    }

    void Start()
    {
        gameOverScreen.SetActive(false);
        levelContainer.SetActive(false);
        startMenu.SetActive(true);
        spaceShip.SetActive(false);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Invader"))
        {
            Destroy(trigger.gameObject);
            GameOver();
        }
    }

    public void StartGame()
    {
        Debug.Log("Start");
        levelContainer.SetActive(true);
        startMenu.SetActive(false);
        spaceShip.SetActive(true);
        currentLevel = 1;
        levelManager.ChoosenLevel(currentLevel);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
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
