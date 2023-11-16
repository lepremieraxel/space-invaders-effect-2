using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    private GameObject levelContainer;
    private GameObject gameOverScreen;
    private GameObject startMenu;
    private GameObject spaceShip;
    void Awake()
    {
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
}
