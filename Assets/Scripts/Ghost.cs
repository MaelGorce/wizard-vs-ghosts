using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Ghost : MonoBehaviour
{
    public float speed = 10;
    public float hpMax = 10;
    public float damage;
    private float hp;
    private GameObject player;
    private Vector2 lookDirection;
    private Animator animator;
    private GameManager gameManager;
    private TextMeshProUGUI ghostCountGUI;
    private float gammaMax;
    private UnityEngine.Color color;
    static private int ghostKilledCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        animator.SetBool("IsMoving", true);
        hp = hpMax;
        gammaMax = gameObject.GetComponent<SpriteRenderer>().color.a;
        color = gameObject.GetComponent<SpriteRenderer>().color;
        ghostCountGUI = GameObject.Find("Ghost Count").GetComponent<TextMeshProUGUI>();
        ghostCountGUI.text = "Ghost killed: " + ghostKilledCounter;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO if player is alive
        if (player && gameManager.isPlaying)
        {
            lookDirection = ((player.transform.position + (0.46f * Vector3.up))  - transform.position).normalized;
            SetAnimator(lookDirection);
            transform.Translate(speed * Time.deltaTime * lookDirection);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    void SetAnimator(Vector2 Direction)
    {
        // Set animator acording to direction
        float absX = Math.Abs(Direction.x);
        float absY = Math.Abs(Direction.y);
        float maxXY = Math.Max(absX,absY);
        if (maxXY == absX)
        {
            if(Direction.x < 0)
                animator.SetInteger("Direction", 3);
            else
                animator.SetInteger("Direction", 2);
        }
        else
        {
            if (Direction.y < 0)
                animator.SetInteger("Direction", 0);
            else
                animator.SetInteger("Direction", 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            Destroy(collision.gameObject);
            Damaged(collision.gameObject.GetComponent<Spell>().damage);
        }
        else if (collision.gameObject.CompareTag("FireSlash"))
        {
            Damaged(collision.gameObject.GetComponent<Spell>().damage);
        }
    }
    private void Damaged(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            ghostCountGUI.text = "Ghost killed: " + ++ghostKilledCounter;
            Destroy(gameObject);
        }
        else
        {
            color.a = hp / hpMax * gammaMax;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
