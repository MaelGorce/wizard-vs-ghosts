using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float livingDuration;
    public float damage;
    public float speed;
    public float coolDown;
    public float size = 1.0f;
    public int spellNumber;
    protected AudioManager audioManager;
    protected GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale *= size;
    }
    protected void SpellInit()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySpellAudioClip(spellNumber);
        StartCoroutine(TimeDestruction());
    }

    IEnumerator TimeDestruction()
    {
        yield return new WaitForSeconds(livingDuration);
        Destroy(gameObject);
    }
}
