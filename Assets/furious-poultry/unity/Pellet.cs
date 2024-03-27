using System;
using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pellet:MonoBehaviour
    {
        Rigidbody _rigidbody;
        float _damage;
        
        public void Initialize(float pelletdamage)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _damage = pelletdamage;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<WarthogAuthoring>(out WarthogAuthoring warthogAuthoring))
            {
                warthogAuthoring.Warthog.Damage(_damage);
            }
        }
        public void AddForce(Vector3 shotForce)
        {
            _rigidbody.AddForce(shotForce);
        }
    }
}