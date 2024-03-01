using com.github.UnityWorkshop.furious_poultry.domain;
using furious_poultry.unity;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PoultryAuthoring : MonoBehaviour, IDestructionProvider
    {
        public PoultryDefinition PoultryDefinition;
        private Rigidbody _rigidbody;
        protected Poultry Poultry;
        
        
        private void FixedUpdate()  
        {
            Poultry.Tick();
        }

        private void OnCollisionEnter(Collision collision)
        {
            WarthogAuthoring enemyAuthoring = collision.gameObject.GetComponent<WarthogAuthoring>();
            if (enemyAuthoring is not null)
            {
                Poultry.CollidedWithEnemy(enemyAuthoring.Warthog);
            }
            
            if (collision.gameObject.CompareTag("Ground"))
            {
                Poultry.CollidedWithGround();
            }
        }
        public void AddForce(Vector3 directionalForce)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(directionalForce);
        }

        //public abstract Rigidbody PoultryAbstractRigidBody();
        public abstract bool IsDead();

        public abstract void DoPrimaryAbility();
        public void Destruct()
        {
            Destroy(gameObject);
        }

        public void Initialize()
        {
            Poultry = PoultryDefinition.ToPoultry(this);
        }
    }
}
