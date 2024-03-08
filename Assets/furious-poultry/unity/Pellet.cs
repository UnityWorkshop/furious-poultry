using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class Pellet:MonoBehaviour
    {
        private Rigidbody body;
        
        public Pellet(Rigidbody pelletPrefab,Transform shotPosition,Quaternion rotationQuaternion)
        {
            body = Instantiate(pelletPrefab, shotPosition.position, rotationQuaternion);
        }

        public void AddForce(Vector3 force)
        {
            body.AddForce(force);
        }

        
    }
}