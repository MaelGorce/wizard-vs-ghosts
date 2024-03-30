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

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        startingScreen.SetActive(true);
        infoGameScreen.SetActive(true);
        playingScreen.SetActive(false);
        choosingScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (isPlaying)
            PauseGame(true);
        else
            PauseGame(false);
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

    public void ChoosingState(int intensity)
    {
        choosingScreen.SetActive(true);
        GenerateUpgradeChoices(intensity);
        PauseGame(true);
    }

    private void GenerateUpgradeChoices(int intensity)
    {
        choosingScreen.GetComponent<ChoiceBuilder>().GenerateRandomChoices((EChoosingIntensity)((spawnManager.GetComponent<SpawnManager>().waveNumber -1) % (int)EChoosingIntensity.eIntensityMax));
    }
    public void ChoiceUpgradeMade()
    {
        PauseGame(false);
        choosingScreen.SetActive(false);
        isPlaying = true;
        spawnManager.GetComponent<SpawnManager>().StartWave();
    }
}
