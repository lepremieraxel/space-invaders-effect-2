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
    // quand on clique sur start, lance le level 1
    {
        Debug.Log("Start1");
        levelContainer.SetActive(true);
        Debug.Log("Start2");
        startMenu.SetActive(false);
        Debug.Log("Start3");
        currentLevel = 1;
        Debug.Log("Start4");
        levelManager.ChoosenLevel(currentLevel);
        Debug.Log("Start5");
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
