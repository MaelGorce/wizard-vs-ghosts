using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Altar : MonoBehaviour
{
    public float loading;
    public float LoadingTime;
    private bool playerInAltar;
    void Start()
    {
        loading = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInAltar = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInAltar = false;
    }

    private void Update()
    {
        if (playerInAltar)
        {
            loading += Time.deltaTime;
        }
    }
    public void Reset()
    {
        loading = 0;
    }
}

