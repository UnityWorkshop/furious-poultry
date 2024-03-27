using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class Beagle : PoultryAuthoring
    {
        [SerializeField] private int pelletAmount = 9;
        [SerializeField] private Rigidbody pelletPrefab;
        [SerializeField] private float pelletSpeed = 1;// 1 = temp
        [SerializeField] private float pelletSpread = 0.1f;
        [SerializeField] private float pelletDamage = 0.1f;
        [SerializeField] private Transform shotPosition;
    
        private bool _abilityUsed;
        
        public override bool IsDead()
        {
            return Poultry.IsOnGround;
        }

        public override void DoPrimaryAbility(Vector3 direction)
        {
            if (_abilityUsed) return;
            _abilityUsed = true;
            for (int i = 0; i < pelletAmount; i++) // not optimal
            {
                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.x += RandomRotationDeviation();
                rotation.y += RandomRotationDeviation();
                rotation.z += RandomRotationDeviation();
                Quaternion rotationQuaternion = Quaternion.Euler(rotation);
                Rigidbody pellet = Instantiate(pelletPrefab, shotPosition.position, rotationQuaternion);
                Pellet component = pellet.GetComponent<Pellet>();
                component.Initialize(pelletDamage);
                Vector3 shotForce = direction * pelletSpeed ;
                pellet.AddForce(shotForce);
            }
        }


        private float RandomRotationDeviation()
        {
            return Random.Range(-pelletSpread, pelletSpread);
        }
        
    }
}
