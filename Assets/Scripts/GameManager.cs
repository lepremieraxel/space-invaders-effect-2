using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentLevel = 0;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject levelContainer;
    private GameObject levelTitle;

    void Awake()
    {
        startButton = GameObject.Find("Start");
        levelContainer = GameObject.Find("Level");
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelTitle = GameObject.Find("LevelTitle");
    }
    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(true);
        levelContainer.SetActive(false);
        levelTitle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    // quand on clique sur start, lance le level 1
    {
        Debug.Log("start");
        currentLevel = 1;
        startButton.SetActive(false);
        levelContainer.SetActive(true);
        levelManager.ChoosenLevel(currentLevel);
    }
}
