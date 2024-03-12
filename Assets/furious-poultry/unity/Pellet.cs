using System;
using com.github.UnityWorkshop.furious_poultry.domain;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class Pellet:MonoBehaviour
    {
        private Rigidbody body;
        private float damage;
        
        public void Instantiate(float pelletdamage)
        {
            damage = pelletdamage;
            
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<WarthogAuthoring>(out WarthogAuthoring warthogAuthoring))
            {
                warthogAuthoring.Warthog.Damage(damage);
            }
        }
    }
}