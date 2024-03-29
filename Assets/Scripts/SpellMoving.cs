using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellMoving : Spell
{
    void Start()
    {
        SpellInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPlaying)
            transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    IEnumerator TimeDestruction()
    {
        yield return new WaitForSeconds(livingDuration);
        Destroy(gameObject);
    }
}


