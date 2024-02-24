using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private Rigidbody pelletRigidbody;
    private void OnValidate()
    {
        pelletRigidbody = GetComponent<Rigidbody>();
        PelletCollisionIgnore();
    }

    private void PelletCollisionIgnore()
    {
        GameObject[] activePellets = GameObject.FindGameObjectsWithTag("AbilityLeftovers");
        foreach (GameObject pellet in activePellets)
        {
            Physics.IgnoreCollision(pelletRigidbody.GetComponent<Collider>(), pellet.GetComponent<Collider>());
        }
    }
    
}
