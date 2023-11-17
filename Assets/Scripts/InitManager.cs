using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    private GameObject levelContainer;
    private GameObject gameOverScreen;
    private GameObject startMenu;
    private GameObject optionMenu;
    private GameObject highscoreMenu;
    private GameObject spaceShip;
    void Awake()
    {
        startMenu = GameObject.Find("StartMenu");
        optionMenu = GameObject.Find("OptionMenu");
        highscoreMenu = GameObject.Find("HighScoreMenu");
        levelContainer = GameObject.Find("Level");
        gameOverScreen = GameObject.Find("GameOverScreen");
        spaceShip = GameObject.Find("SpaceShip");
    }
    void Start()
    {
        gameOverScreen.SetActive(false);
        levelContainer.SetActive(false);
        startMenu.SetActive(true);
        optionMenu.SetActive(false);
        highscoreMenu.SetActive(false);
        spaceShip.SetActive(false);
    }
}
