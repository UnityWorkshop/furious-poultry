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

        public override void DoPrimaryAbility()
        {
        
            ShootShotgun();
        
        }
    

        private void ShootShotgun()
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
                Rigidbody pellet = Instantiate(pelletPrefab, transform.position, rotationQuaternion);
                Pellet component = pellet.GetComponent<Pellet>();
                component.Instantiate(pelletDamage);
                Vector3 shotForce = pellet.transform.forward * pelletSpeed ;
                pellet.AddForce(shotForce);
            }
        }

        private float RandomRotationDeviation()
        {
            return Random.Range(-pelletSpread, pelletSpread);
        }
        
    }
}
