using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poultry : MonoBehaviour
{
    [SerializeField] private int damage;
    public bool isOnGround = false;

    private void OnCollisionEnter(Collision collision)
    {
        Enemies collidedEnemyScript = collision.gameObject.GetComponent<Enemies>();
        if (collidedEnemyScript is not null)
            collidedEnemyScript.Damage(damage);
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("hit ground"); 
            isOnGround = true;
            //death here
        }
    }
}
