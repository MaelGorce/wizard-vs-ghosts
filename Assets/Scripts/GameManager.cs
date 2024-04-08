using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Choice;

public class GameManager : MonoBehaviour
{

    public bool isPlaying;
    [SerializeField] private GameObject startingScreen;
    [SerializeField] private GameObject infoGameScreen;
    [SerializeField] private GameObject playingScreen;
    [SerializeField] private GameObject choosingScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject[] upgradeChoices;
    [SerializeField] private GameObject spawnManager;
    private bool isChoosing;
    public EChoosingIntensity choiceIntensity { get; private set; }

    public static GameManager instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            isPlaying = false;
            startingScreen.SetActive(true);
            infoGameScreen.SetActive(true);
            playingScreen.SetActive(false);
            choosingScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            isChoosing = false;
        }
    }

    public void StartGame()
    {
        isPlaying = true;
        startingScreen.SetActive(false);
        playingScreen.SetActive(true);
        spawnManager.GetComponent<SpawnManager>().StartWave();
    }
    public void PauseGame(bool pause)
    {
        if(pause)
        {
            Debug.Log("Game Paused");
            isPlaying = false;
        }
        else
        {
            Debug.Log("Game Unpaused");
            isPlaying =true;
        }
    }

    public void PauseToggle()
    {
        if (!isChoosing)
        {
            if (isPlaying)
                PauseGame(true);
            else
                PauseGame(false);
        }
    }

    public void GameOver()
    {
        isPlaying = false;
        Debug.Log("Game Over");
        gameOverScreen.SetActive(true);
    }
    public void RestartGame()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChoosingState(EChoosingIntensity intensity)
    {
        PauseGame(true);
        isChoosing = true;
        choosingScreen.SetActive(true);
        choiceIntensity = intensity;
        if (ChoiceBuilder.instance)
            ChoiceBuilder.instance.GenerateRandomChoices();
    }
    public void ChoiceUpgradeMade()
    {
        isChoosing = false;
        PauseGame(false);
        choosingScreen.SetActive(false);
        isPlaying = true;
        spawnManager.GetComponent<SpawnManager>().StartWave();
    }
}
