using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Altar : MonoBehaviour
{
    public float LoadingTime;
    [SerializeField] public float loading { get; private set; }
    private bool playerInAltar;
    void Start()
    {
        Reset();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            playerInAltar = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInAltar = false;
    }

    private void Update()
    {
        if (playerInAltar && loading < LoadingTime)
        {
            loading += Time.deltaTime;
        }
    }
    public void Reset()
    {
        loading = 0;
    }
}

