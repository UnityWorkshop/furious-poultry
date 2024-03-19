using System.Collections.Generic;
using com.github.UnityWorkshop.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.application.services;
using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.unity.definition;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

namespace com.github.UnityWorkshop.furious_poultry.unity.authoring
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PoultryAuthoring : MonoBehaviour, IDestructionProvider
    {
        public PoultryDefinition poultryDefinition;
        private Rigidbody _rigidbody;
        protected Poultry Poultry;
        protected PoultryService PoultryService;
        
        [FormerlySerializedAs("AbilityLeftOvers")] public List<Pellet> abilityLeftOvers = new List<Pellet>();
        
        
        private void FixedUpdate()  
        {
            PoultryService.Tick();
        }

        private void OnCollisionEnter(Collision collision)
        {
            WarthogAuthoring enemyAuthoring = collision.gameObject.GetComponent<WarthogAuthoring>();
            if (enemyAuthoring is not null)
            {
                PoultryService.CollidedWithEnemy(enemyAuthoring.Warthog);
            }
            
            if (collision.gameObject.CompareTag("Ground"))
            {
                PoultryService.CollidedWithGround();
            }
            
            PoultryService.CollidedWithThing();
        }
        public void AddForce(Vector3 directionalForce)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(directionalForce);
        }

        //public abstract Rigidbody PoultryAbstractRigidBody();
        public bool IsDead()
        {
            return Poultry.IsDead;
        }

        public abstract void DoPrimaryAbility();
        public void Destruct()
        {
            Destroy(gameObject);
        }

        public void Initialize()
        {
            Poultry = poultryDefinition.ToPoultry();
            PoultryService = new PoultryService(this, Poultry);
        }
    }
}