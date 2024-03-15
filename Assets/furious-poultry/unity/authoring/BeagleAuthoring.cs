using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class BeagleAuthoring : PoultryAuthoring
    {
        [SerializeField] private int pelletAmount = 9;
        [SerializeField] private Rigidbody pelletPrefab;
        [SerializeField] private float pelletSpeed = 1;// 1 = temp
        [SerializeField] private float pelletSpread = 0.1f;
        [SerializeField] private Transform shotPosition;
    
        private bool _abilityUsed;

        public override void DoPrimaryAbility()
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
                Vector3 shotForce = pellet.transform.forward * pelletSpeed ;
                pellet.AddForce(shotForce);
            }
        }

        private float RandomRotationDeviation()
        {
            return Random.Range(-pelletSpread, pelletSpread);
        }

        private Vector3 ShotPosition()
        {
            return transform.position ; // todo: transform.position + buffer distance in viewing direction
        }
    }
}
