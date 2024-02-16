using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poultry : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int health = 100;
    [SerializeField] private bool isOnGround = false;
    [SerializeField] private bool hasCollided = false;
    [SerializeField] private int decayTickDamage = 1;   //120f / 60f; (test) // float appears to not work correctly, always defaulting to smt around 0.001 or smt, int works but it's quick

    private void FixedUpdate()
    {
        if (hasCollided)
        {
            health -= decayTickDamage;
        }
        if (health <= 0)
        {
            DestroyPoultry();
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
            DestroyPoultry();
        }
    }

    private void DestroyPoultry()
    {
        Destroy(gameObject);   
            /*
            insert death animation
            and some animation so it wont be a harsh jump cut
            */
        
    }
}
