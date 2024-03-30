using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawningTombs;
    [SerializeField] private GameObject[] ghosts;
    [SerializeField] private Wave[] wavesConfig;
    [SerializeField] private GameObject altarObject;
    [SerializeField] private GameObject altarBarObject;
    [SerializeField] private GameObject WaveUIObject;
    [SerializeField] private TextMeshProUGUI WaveCountUI;
    private Altar altar;
    private Vector2[] spawningTombsPos;
    public int waveNumber = 0;
    private bool waitForSpawn = false;
    private GameManager gameManager;
    private int randomTomb;
    private int randomGhost;
    private float randomForGhost;
    private float sumValuesSpawn;
    private Vector3 altarBarScale;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        altar = altarObject.GetComponent<Altar>();
        // Init tombs positions
        spawningTombsPos = new Vector2[spawningTombs.Length];
        for (int i = 0; i< spawningTombs.Length; ++i )
        {
            spawningTombsPos[i] = spawningTombs[i].transform.position;
        }
        altarBarScale = new Vector3(0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // State Machine of spawning waves
        if (gameManager.isPlaying)
        {
            altarBarScale.x = altar.loading / altar.LoadingTime;
            altarBarObject.GetComponent<RectTransform>().localScale = altarBarScale;
            if (altar.loading > altar.LoadingTime)
            {
                GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
                foreach (GameObject ghost in ghosts)
                    Destroy(ghost);
                gameManager.ChoosingState(0);
                waitForSpawn = true;

            }
            else if(!waitForSpawn)
            {
                // Spawn Something
                RandomGhostSpawn(1);
                waitForSpawn = true;
                Invoke("CoolDownSpawn", wavesConfig[waveNumber].spawningTime);
            }
        }
    }
    public void StartWave()
    {
        ++waveNumber;
        Debug.Log("Starting Wave " + waveNumber);
        WaveUIObject.GetComponent<WaveUI>().DisplayWave(waveNumber);
        WaveCountUI.text = "Wave " + waveNumber;
        //Wait before wave start
        altar.loading = 0;
        altar.LoadingTime = wavesConfig[waveNumber].altarLoadingTime;
        waitForSpawn = true;
        audioManager.PlayNarratorAudioClip(wavesConfig[waveNumber].startingWaveAudioClip);
        Invoke("CoolDownSpawn", 1);
        // prepare variables for ghost spawn
        sumValuesSpawn = 0;
        for (int i = 0; i< ghosts.Length; ++i) {
            sumValuesSpawn += wavesConfig[waveNumber].waveSpawnValueOfGhosts[i];
        }
    }

    void CoolDownSpawn()
    {
        waitForSpawn = false;
    }

    void RandomGhostSpawn(int nbOfGhosts)
    {
        for (int i = 0; i < nbOfGhosts; ++i)
        {
            randomTomb = Random.Range(0, spawningTombs.Length);
            randomForGhost = Random.Range(0.0f, sumValuesSpawn);
            float minCompare = 0;
            for (int j = 0; j < ghosts.Length; ++j)
            {
                if (randomForGhost > minCompare &&
                    randomForGhost < minCompare + wavesConfig[waveNumber].waveSpawnValueOfGhosts[j])
                {
                    randomGhost = j;
                    break;
                }
                else
                {
                    minCompare += wavesConfig[waveNumber].waveSpawnValueOfGhosts[j];
                }
            }
            Instantiate(ghosts[randomGhost], spawningTombsPos[randomTomb], ghosts[randomGhost].transform.rotation);
        }
    }
}
