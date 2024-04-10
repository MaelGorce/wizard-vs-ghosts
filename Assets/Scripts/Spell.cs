using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float livingDuration;
    public float damage ;
    public float speed;
    public float coolDown;
    public float size = 1.0f;
    private float livingDurationInit;
    private float damageInit;
    private float speedInit;
    private float coolDownInit;
    private float sizeInit;
    public bool alreadyLoadedInit = false;
    [SerializeField] protected int spellNumber;

    // Start is called before the first frame update
    void Start()
    {
        SpellInit();
    }

    void Update()
    {
        if (GameManager.instance)
            if (GameManager.instance.isPlaying)
                transform.Translate(speed * Time.deltaTime * Vector2.up);
    }
    protected void SpellInit()
    {
        transform.localScale *= size;
        if (GameManager.instance.isPlaying)
        {
            AudioManager.instance.PlaySpellAudioClip(spellNumber);
            StartCoroutine(TimeDestruction());
        }
    }

    IEnumerator TimeDestruction()
    {
        yield return new WaitForSeconds(livingDuration);
        if (gameObject)
            Destroy(gameObject);
    }

    public void StoreInitalValues()
    {
        if(alreadyLoadedInit)
        {
            livingDuration= livingDurationInit;
            damage = damageInit;
            speed = speedInit;
            coolDown = coolDownInit;
            size = sizeInit;
        }
        else
        {
            livingDurationInit = livingDuration;
            damageInit = damage;
            speedInit = speed;
            coolDownInit = coolDown;
            sizeInit = size;
            alreadyLoadedInit = true;
        }
    }

    // Called when spell is hitting a ghost
    public virtual void SpellHit()
    {
        // Nothing happens for the spell
    }
}
