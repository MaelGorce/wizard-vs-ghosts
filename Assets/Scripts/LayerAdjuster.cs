using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerAdjuster : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
        void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y)
            spriteRenderer.sortingLayerName = "ArmSpellLayer";
        else
            spriteRenderer.sortingLayerName = "WalkingLayer";
    }
}
