using System;
using com.github.UnityWorkshop.furious_poultry.domain;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pellet:MonoBehaviour
    {
        private float damage;
        
        public void initialize(float pelletdamage, float pelletspeed, Vector3 pelletrotation)
        {
            damage = pelletdamage;
            GetComponent<Rigidbody>().AddForce(pelletspeed*pelletrotation);
        }
        
        

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<WarthogAuthoring>(out WarthogAuthoring warthog))
            {
                warthog.Damage(damage);
            }
        }
    }
}