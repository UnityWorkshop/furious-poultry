using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Poultry : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private int health = 100;
        [SerializeField] protected bool isOnGround;
        [SerializeField] private bool hasCollided;
        [SerializeField] private int decayTickDamage = 1;   //120f / 60f; (test) // float appears to not work correctly, always defaulting to smt around 0.001 or smt, int works but it's quick
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
            Warthog collidedWarthogScript = collision.gameObject.GetComponent<Warthog>();
            if (collidedWarthogScript is not null)
            {
                collidedWarthogScript.Damage(damage);
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
