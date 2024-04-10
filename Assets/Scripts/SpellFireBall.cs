using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class SpellFireBall : Spell
{
    // POLYMORPHISM
    public override void SpellHit()
    {
        // The fireball destroy itself on collision on ghosts
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PropsObstacle"))
        {
            Destroy(gameObject);
        }
    }
}
