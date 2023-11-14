using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentLevel;
    private LevelManager levelManager;
    private GameObject levelContainer;
    private GameObject gameOverScreen;
    private GameObject startMenu;
    private GameObject gameUI;

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        startMenu = GameObject.Find("StartMenu");
        levelContainer = GameObject.Find("Level");
        gameOverScreen = GameObject.Find("GameOverScreen");
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        levelContainer.SetActive(false);
        startMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        currentLevel = 1;
        levelManager.ChoosenLevel(currentLevel);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        gameOverScreen.SetActive(true);
    }

    public void StartMenu() {
        startMenu.SetActive(true);
    }
}
