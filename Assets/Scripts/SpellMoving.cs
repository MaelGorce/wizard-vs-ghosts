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
        if(GameManager.instance)
            if (GameManager.instance.isPlaying)
                transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    IEnumerator TimeDestruction()
    {
        yield return new WaitForSeconds(livingDuration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spellNumber == 1 && collision.CompareTag("PropsObstacle"))
        {
            Destroy(gameObject);
        }
    }
}


