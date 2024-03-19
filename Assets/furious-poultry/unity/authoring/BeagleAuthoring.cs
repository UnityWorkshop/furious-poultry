
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity.authoring
{
    public class BeagleAuthoring : PoultryAuthoring
    {
        [SerializeField] private int pelletAmount = 9;
        [SerializeField] private Pellet pelletPrefab;
        [SerializeField] private float pelletSpeed = 1;// 1 = temp
        [SerializeField] private float pelletSpread = 0.1f;
        [SerializeField] private float pelletDamage = 0.1f;
        [SerializeField] private Transform shotPosition;
    
        private bool _abilityUsed;
        
        public override void DoPrimaryAbility()
        {
            if (_abilityUsed) return;
            abilityLeftOvers.Clear();
            _abilityUsed = true;
            for (int i = 0; i < pelletAmount; i++) // not optimal
            {
                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.x += RandomRotationDeviation();
                rotation.y += RandomRotationDeviation();
                rotation.z += RandomRotationDeviation();
                Quaternion rotationQuaternion = Quaternion.Euler(rotation);
                Pellet pellet = Instantiate(pelletPrefab, shotPosition.position, rotationQuaternion);
                abilityLeftOvers.Add(pellet);
                pellet.initialize(pelletDamage, pelletSpeed, rotation);
            }
        }

        private float RandomRotationDeviation()
        {
            return Random.Range(-pelletSpread, pelletSpread);
        }
        
    }
}
