using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beagle : Poultry
{
    public override bool IsDead()
    {
        return isOnGround;
    }
}
