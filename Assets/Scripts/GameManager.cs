using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int latestScore;
    private int currentLevel;
    private string webRequestUrlPost = "https://space-invaders-effect.axelmarcial.com/index.php";
    private string webRequestUrlGet = "https://space-invaders-effect.axelmarcial.com/scores.txt";
    private LevelManager levelManager;
    private GameObject levelContainer;
    private GameObject gameOverScreen;
    private GameObject startMenu;
    private GameObject optionMenu;
    private GameObject highscoreMenu;
    private GameObject spaceShip;
    private GameObject invadersParent;
    private Text latestScoreText;
    private SpaceShipManager spaceShipManager;
    private AudioSource audioMainCamera;
    private AudioManager audioManager;
    private Text usernameInput;
    private Text statusPost;

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        spaceShipManager = GameObject.Find("SpaceShipManager").GetComponent<SpaceShipManager>();
        startMenu = GameObject.Find("StartMenu");
        optionMenu = GameObject.Find("OptionMenu");
        highscoreMenu = GameObject.Find("HighScoreMenu");
        levelContainer = GameObject.Find("Level");
        gameOverScreen = GameObject.Find("GameOverScreen");
        spaceShip = GameObject.Find("SpaceShip");
        invadersParent = GameObject.Find("Invaders");
        latestScoreText = GameObject.Find("LatestScore").GetComponent<Text>();
        audioMainCamera = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audioManager = GameObject.Find("Main Camera").GetComponent<AudioManager>();
        usernameInput = GameObject.Find("InputNameText").GetComponent<Text>();
        statusPost = GameObject.Find("StatusPost").GetComponent<Text>();
    }

    void Start()
    {
        StartCoroutine(GetHighScores());
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
        optionMenu.SetActive(false);
        highscoreMenu.SetActive(false);
        spaceShip.SetActive(true);
        audioMainCamera.clip = audioManager.musicGame;
        audioMainCamera.Play();
        currentLevel = 1;
        levelManager.currentScore = 0;
        levelManager.stoppedCoroutine = false;
        levelManager.ChoosenLevel(currentLevel);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        latestScore = levelManager.currentScore;
        latestScoreText.text = "Your Score : " + latestScore.ToString();
        spaceShipManager.canControlShip = false;
        gameOverScreen.SetActive(true);
        levelContainer.SetActive(false);
        startMenu.SetActive(false);
        spaceShip.SetActive(false);
        optionMenu.SetActive(false);
        highscoreMenu.SetActive(false);
    }

    public void StartMenu()
    {
        startMenu.SetActive(true);
        levelContainer.SetActive(false);
        optionMenu.SetActive(false);
        highscoreMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        spaceShip.SetActive(false);
        audioMainCamera.clip = audioManager.menuGame;
        audioMainCamera.Play();
    }
    public void OptionMenu()
    {
        optionMenu.SetActive(true);
        startMenu.SetActive(false);
        highscoreMenu.SetActive(false);
        levelContainer.SetActive(false);
        gameOverScreen.SetActive(false);
        spaceShip.SetActive(false);
    }
    public void HighscoreMenu()
    {
        highscoreMenu.SetActive(true);
        startMenu.SetActive(false);
        optionMenu.SetActive(false);
        levelContainer.SetActive(false);
        gameOverScreen.SetActive(false);
        spaceShip.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PostHighScoreWithUsername()
    {
        StartCoroutine(SetHighScore(usernameInput.text, latestScore));
    }

    IEnumerator GetHighScores()
    {
        using(UnityWebRequest getRequest = UnityWebRequest.Get(webRequestUrlGet))
        {
            yield return getRequest.SendWebRequest();
            Debug.Log(getRequest.responseCode);
        }
    }
    IEnumerator SetHighScore(string username, int score)
    {
        WWWForm postData = new WWWForm();
        postData.AddField("username", username);
        postData.AddField("score", score.ToString());

        UnityWebRequest www = UnityWebRequest.Post(webRequestUrlPost, postData);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            statusPost.text = "An error occured.";
            statusPost.color = Color.red;
        }
        else
        {
            Debug.Log(www.result);
            statusPost.text = "High-score successfully saved.";
            Color tmpColor = Color.white;
            tmpColor.r = 0.2941177f;
            tmpColor.g = 0.9058824f;
            tmpColor.b = 0.2352941f;
            statusPost.color = tmpColor;
        }
    }
}
