using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Choice;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawningTombs;
    [SerializeField] private GameObject[] ghosts;
    [SerializeField] private Wave[] wavesConfig;
    [SerializeField] private GameObject altarBarObject;
    [SerializeField] private GameObject WaveUIObject;
    [SerializeField] private TextMeshProUGUI WaveCountUI;
    [SerializeField] private Altar altar;
    private Vector2[] spawningTombsPos;
    public int waveNumber = 0;
    private bool waitForSpawn = false;
    private int randomTomb;
    private int randomGhost;
    private float randomForGhost;
    private float sumValuesSpawn;
    private Vector3 altarBarScale;

    private float minCompare;
    private bool altarLoaded;

    // Start is called before the first frame update
    void Start()
    {

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
        if (GameManager.instance.isPlaying)
        {
            altarBarScale.x = altar.loading / altar.LoadingTime;
            altarBarObject.GetComponent<RectTransform>().localScale = altarBarScale;
            if (altarLoaded)
            {
                if(GameObject.FindGameObjectsWithTag("Ghost").Length == 0)
                {
                    GameManager.instance.ChoosingState((EChoosingIntensity)((waveNumber - 1) % (int)EChoosingIntensity.eIntensityMax));
                    waitForSpawn = true;
                }

            }
            if (altar.loading > altar.LoadingTime)
            {
                altarLoaded = true;
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
        altar.Reset();
        altar.LoadingTime = wavesConfig[waveNumber].altarLoadingTime;
        waitForSpawn = true;
        AudioManager.instance.PlayNarratorAudioClip(wavesConfig[waveNumber].startingWaveAudioClip);
        Invoke("CoolDownSpawn", 1);
        altarLoaded = false;
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
            minCompare = 0;
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
