using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poultry : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float health = 100f;
    [SerializeField] private bool isOnGround = false;
    [SerializeField] private bool hasCollided = false;
    [SerializeField] private float decayTickDamage = 1f / 60f;

    // todo: timed life -> reduce hp by x every second after collision
    private void FixedUpdate()
    {
        if (hasCollided)
        {
            health -= decayTickDamage;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasCollided = true;
        Enemies collidedEnemyScript = collision.gameObject.GetComponent<Enemies>();
        if (collidedEnemyScript is not null)
        {
            collidedEnemyScript.Damage(damage);
            health -= damage;
        }
            
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("hit ground"); 
            isOnGround = true;
            //death here
        }

        
            
        
    }
}
