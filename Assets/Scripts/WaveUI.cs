using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float livingDuration;
    [SerializeField] private TextMeshProUGUI textTMP;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayWave(int waveNumber)
    {
        textTMP.text = "Wave " + waveNumber;
        gameObject.SetActive(true);
        StartCoroutine(TimeDesactivate());
    }

    IEnumerator TimeDesactivate()
    {
        yield return new WaitForSeconds(livingDuration);
        gameObject.SetActive(false);
    }
}
