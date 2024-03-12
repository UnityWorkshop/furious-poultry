using System;
using com.github.UnityWorkshop.furious_poultry.domain;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class Pellet:MonoBehaviour
    {
        private Rigidbody body;
        private float damage;
        
        public Pellet(Rigidbody pelletPrefab,Vector3 shotPosition,Quaternion rotationQuaternion, float pelletdamage, float pelletSpeed)
        {
            body = Instantiate(pelletPrefab, shotPosition, rotationQuaternion);
            damage = pelletdamage;
            Vector3 shotForce = transform.forward * pelletSpeed ;
            AddForce(shotForce);
        }

        public void AddForce(Vector3 force)
        {
            body.AddForce(force);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Warthog>(out Warthog warthog))
            {
                warthog.Damage(damage);
            }
        }
    }
}