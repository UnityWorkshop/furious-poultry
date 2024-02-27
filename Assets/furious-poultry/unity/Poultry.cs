using furious_poultry.unity;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Poultry : MonoBehaviour
    {
        public PoultryDefinition;
        private Rigidbody _rigidbody;
        public void OnValidate()
        {
            //_rigidbody = GetComponent<Rigidbody>();
            // _rigidbody = PoultryAbstractRigidBody();
        }

    

        private void FixedUpdate()  
        {
            if (hasCollided)
            {
                health -= decayTickDamage;
            }
            if (health <= 0)
            {
                DestroyPoultry();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            hasCollided = true;
            Enemies collidedEnemyScript = collision.gameObject.GetComponent<Enemies>();
            if (collidedEnemyScript is not null)
            {
                collidedEnemyScript.Damage(damage);
                health -= damage;
            }
            
            if (collision.gameObject.CompareTag("Ground"))
            {
                //Debug.Log("hit ground"); 
                isOnGround = true;
                DestroyPoultry();
            }
        }

        public void DestroyPoultry()
        {
            Destroy(gameObject);   
            /*
            insert death animation
            and some animation so it wont be a harsh jump cut
            */
        
        }

        public void AddForce(Vector3 directionalForce)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(directionalForce);
        }

        //public abstract Rigidbody PoultryAbstractRigidBody();
        public abstract bool IsDead();

        public abstract void DoPrimaryAbility();
    }
}
