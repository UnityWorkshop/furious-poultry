using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beagle : Poultry
{
    [SerializeField] private int pelletAmount = 9;
    [SerializeField] private Rigidbody pelletPrefab;
    [SerializeField] private float pelletSpeed = 1;// 1 = temp
    [SerializeField] private float pelletSpread = 0.01f;
        
    
    private bool _abilityUsed;
    
    public override bool IsDead()
    {
        return isOnGround;
    }

    public override void DoPrimaryAbility()
    {
        
        ShootShotgun();
        
    }

    private void ShootShotgun()
    {
        for (int i = 0; i < pelletAmount; i++) // not optimal
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.x += RandomRotationDeviation();
            rotation.y += RandomRotationDeviation();
            rotation.z += RandomRotationDeviation();
            Quaternion rotationQuaternion = Quaternion.Euler(rotation);
            Rigidbody pellet = Instantiate(pelletPrefab, transform.position, rotationQuaternion);
            pellet.AddForce(rotation * pelletSpeed);
        }
    }

    private float RandomRotationDeviation()
    {
        return Random.Range(-pelletSpread, pelletSpread);
    }
}
