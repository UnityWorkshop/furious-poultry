using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poultry : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        Enemies collidedEnemyScript = collision.gameObject.GetComponent<Enemies>();
        if (collidedEnemyScript is not null)
            collidedEnemyScript.Damage(damage);
    }
}
