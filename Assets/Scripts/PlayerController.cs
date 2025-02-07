using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    private Vector2 playerCenter = new Vector2 (0,0.46f);
    private Vector3 spellAngle;

    // Start is called before the first frame update
    void Start()
    {
        movingVector = Vector2.zero;
        spellAngle = Vector3.zero;
        m_hp = hpMax;
        hpBarScale = new Vector3(1, 1, 1);
        Spell1.GetComponent<Spell>().StoreInitalValues();
        Spell2.GetComponent<Spell>().StoreInitalValues();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ABSTRACTION
        if (isAlive && GameManager.instance.isPlaying)
        {
            MoveWithInput();

            if (Input.GetMouseButton(1))
            {
                //Right mouse click
                if (!isAttackingSpell1)
                {
                    FireBall();
                }

            }
            else if (Input.GetMouseButton(0))
            {
                //Left mouse click
                // TODO : Is not on Pause button
                if(!isAttackingSpell2)
                {
                    FireSlash();
                }
            }
        }
        else
            rigidBody.velocity = movingVector * 0; // While in pause

    }

    private void MoveWithInput()
    {
        movingVector.x = Input.GetAxis("Horizontal");
        movingVector.y = Input.GetAxis("Vertical");
        if ((movingVector.x > 0 && child.transform.rotation.y < 0.01) ||
             (movingVector.x < 0 && child.transform.rotation.y > 0.99))
        {
            Debug.Log("ici x : " +  movingVector.x + " ; y : " +movingVector.y);
            child.transform.Rotate(Vector3.up * 180);
        }

        if (movingVector.magnitude > 1)
            movingVector.Normalize();

        rigidBody.velocity = movingVector * speed;
    }

    private void FireBall()
    {
        animator.SetTrigger("attack");
        isAttackingSpell1 = true;
        StartCoroutine(CoolDownSpell1(Spell1.GetComponent<Spell>().coolDown));
        StartCoroutine(Spell1Delayed(0.22f, GetAngleSpell()));
    }
    private void FireSlash()
    {
        animator.SetTrigger("attack02");
        isAttackingSpell2 = true;
        StartCoroutine(CoolDownSpell2(Spell2.GetComponent<Spell>().coolDown));
        StartCoroutine(Spell2Delayed(0.32f, GetAngleSpell()));
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
        if (GameManager.instance.isPlaying)
        {
            spellAngle.z = angle;
            Instantiate(Spell1, (transform.position + (Vector3)playerCenter ), Quaternion.Euler(spellAngle));
        }
    }

    IEnumerator Spell2Delayed(float coolDownTime, float angle)
    {
        yield return new WaitForSeconds(coolDownTime);
        if (GameManager.instance.isPlaying)
        {
            spellAngle.z = angle;
            Instantiate(Spell2, (transform.position + (Vector3)playerCenter), Quaternion.Euler(spellAngle));
        }
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
                AudioManager.instance.PlayPlayerDeathAudioClip();
                animator.SetTrigger("death");
                GameManager.instance.GameOver();
                m_hp = 0;
            }
            else
            {
                AudioManager.instance.PlayPlayerDamageAudioClip();
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
    public void UpgradelivingDurationSpell1(float percent)
    { Spell1.GetComponent<Spell>().livingDuration *= percent; }
    public void UpgradelivingDurationSpell2(float percent)
    { Spell2.GetComponent<Spell>().livingDuration *= percent; }
    public void UpgradeSizeSpell1(float percent)
    { Spell1.GetComponent<Spell>().size *= percent; }
    public void UpgradeSizeSpell2(float percent)
    { Spell2.GetComponent<Spell>().size *= percent; }

}
