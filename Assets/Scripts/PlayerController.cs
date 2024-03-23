using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public Vector2 movingVector;
    public Animator m_Animator;
    public GameObject child;
    public float childRotY;

    // Start is called before the first frame update
    void Start()
    {
        movingVector = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {

        movingVector.x = Input.GetAxis("Horizontal");
        movingVector.y = Input.GetAxis("Vertical");
        childRotY = child.transform.rotation.y;
        if ( (movingVector.x > 0 && child.transform.rotation.y < 0.01) ||
             (movingVector.x < 0 && child.transform.rotation.y > 0.99 ) )
            child.transform.Rotate(Vector3.up * 180);

        if(movingVector.magnitude > 1)
            movingVector.Normalize();

        transform.Translate( movingVector * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.Space))
        {
            m_Animator.SetTrigger("attack");
        }

    }
}
