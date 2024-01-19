using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poultry : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<Enemies>() != null)
        {
            //Debug.Log("is enemy");
            collision.gameObject.GetComponent<Enemies>().Damage(damage);
        }
    }
}
