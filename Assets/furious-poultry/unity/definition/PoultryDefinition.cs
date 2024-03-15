using System;
using com.github.UnityWorkshop.furious_poultry.domain;

namespace com.github.UnityWorkshop.furious_poultry.unity.definition
{   
    [Serializable]
    public class PoultryDefinition
    { 
        public float damage = 10;
        public float health = 100;
        public bool isOnGround;
        public bool hasCollided;
        public float decayTickDamage = 1;

        public Poultry ToPoultry()
        {
            return new Poultry(decayTickDamage, damage, health, hasCollided);
        }

    }
}