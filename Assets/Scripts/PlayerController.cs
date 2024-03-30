using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public bool isAlive = true;

    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float hpMax = 200.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject child;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private GameObject Spell1;
    [SerializeField] private GameObject Spell2;
    [SerializeField] private GameObject hpBarObject;
    

    private Vector3 hpBarScale;
    private float m_hp;
    private Vector2 movingVector;
    private bool isAttackingSpell1 = false;
    private bool isAttackingSpell2 = false;
    private Vector2 playerCenter = new Vector2 (0,0.6f);
    private Vector3 spellAngle;

    private AudioManager audioManager;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        movingVector = Vector2.zero;
        spellAngle = Vector3.zero;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_hp = hpMax;
        hpBarScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive && gameManager.isPlaying)
        {
            movingVector.x = Input.GetAxis("Horizontal");
            movingVector.y = Input.GetAxis("Vertical");
            if ( (movingVector.x > 0 && child.transform.rotation.y < 0.01) ||
                 (movingVector.x < 0 && child.transform.rotation.y > 0.99 ) )
                child.transform.Rotate(Vector3.up * 180);

            if(movingVector.magnitude > 1)
                movingVector.Normalize();
            
            rigidBody.velocity = movingVector * speed;

            if (Input.GetMouseButton(1))
            {
                //Right mouse click
                if (!isAttackingSpell1)
                {
                    animator.SetTrigger("attack");
                    isAttackingSpell1 = true;
                    StartCoroutine(CoolDownSpell1(Spell1.GetComponent<Spell>().coolDown));
                    StartCoroutine(Spell1Delayed(0.22f, GetAngleSpell()));
                }

            }
            else if (Input.GetMouseButton(0))
            {
                //Left mouse click
                if(!isAttackingSpell2)
                {
                    animator.SetTrigger("attack02");
                    isAttackingSpell2 = true;
                    StartCoroutine(CoolDownSpell2(Spell2.GetComponent<Spell>().coolDown));
                    StartCoroutine(Spell2Delayed(0.32f, GetAngleSpell()));
                }
            }
        }
        else
            rigidBody.velocity = movingVector * 0;

    }
    IEnumerator CoolDownSpell1(float coolDownTime)
    {
        yield return new WaitForSeconds(coolDownTime);
        isAttackingSpell1 = false;
    }
    IEnumerator CoolDownSpell2(float coolDownTime)
    {
        yield return new WaitForSeconds(coolDownTime);
        isAttackingSpell2 = false;
    }

    IEnumerator Spell1Delayed(float coolDownTime,float angle)
    {
        yield return new WaitForSeconds(coolDownTime);
        spellAngle.z = angle;
        Instantiate(Spell1, (transform.position + (Vector3)playerCenter), Quaternion.Euler(spellAngle));
    }

    IEnumerator Spell2Delayed(float coolDownTime, float angle)
    {
        yield return new WaitForSeconds(coolDownTime);
        spellAngle.z = angle;
        Instantiate(Spell2, (transform.position + (Vector3)playerCenter), Quaternion.Euler(spellAngle));
    }


    float GetAngleSpell()
    {
        return Vector2.SignedAngle(Vector2.up,
            Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position + (Vector3)playerCenter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost") && isAlive)
        {
            m_hp -= collision.gameObject.GetComponent<Ghost>().damage;
            Destroy(collision.gameObject);

            if (m_hp <= 0)
            {
                isAlive = false;
                rigidBody.velocity = Vector2.zero;
                audioManager.PlayPlayerDeathAudioClip();
                animator.SetTrigger("death");
                gameManager.GameOver();
                m_hp = 0;
            }
            else
            {
                audioManager.PlayPlayerDamageAudioClip();
                animator.SetTrigger("damage");
            }


            hpBarScale.x = m_hp / hpMax;
            hpBarObject.GetComponent<RectTransform>().localScale = hpBarScale;
        }

    }
    public void Heal(int hp)
    {
        if (m_hp + hp > hpMax)
            m_hp = hpMax;
        else
            m_hp += hp;
    }
    // Upgrades 
    public void UpgradeHP(float percent)
    {
        hpMax *= percent;
        m_hp *= percent;
        hpBarScale.x = m_hp / hpMax;
        hpBarObject.GetComponent<RectTransform>().localScale = hpBarScale;
    }

    public void UpgradeSpeed(float percent)
    { speed *= percent;}

    // Spell1 = fireball
    // Spell2 = Fire Slash
    public void UpgradeCDSpell1(float percent)
    { Spell1.GetComponent<Spell>().coolDown /= percent; }
    public void UpgradeCDSpell2(float percent)
    { Spell2.GetComponent<Spell>().coolDown /= percent; }
    public void UpgradeDmgSpell1(float percent)
    { Spell1.GetComponent<Spell>().damage *= percent; }
    public void UpgradeDmgSpell2(float percent)
    { Spell2.GetComponent<Spell>().damage *= percent; }
    public void UpgradeSpeedSpell2(float percent)
    { Spell2.GetComponent<Spell>().speed *= percent; }
    public void UpgradelivingDurationSpell1(float percent)
    { Spell1.GetComponent<Spell>().livingDuration *= percent; }
    public void UpgradelivingDurationSpell2(float percent)
    { Spell2.GetComponent<Spell>().livingDuration *= percent; }
    public void UpgradeSizeSpell1(float percent)
    { Spell1.GetComponent<Spell>().size *= percent; }
    public void UpgradeSizeSpell2(float percent)
    { Spell2.GetComponent<Spell>().size *= percent; }

}
