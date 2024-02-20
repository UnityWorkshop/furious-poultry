using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beagle : Poultry
{
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
        // pew pew here
        // how do i shotgun?
    }
}
