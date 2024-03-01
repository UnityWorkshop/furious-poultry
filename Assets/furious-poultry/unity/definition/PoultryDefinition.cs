using System;
using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.unity;

namespace furious_poultry.unity
{   
    [Serializable]
    public class PoultryDefinition
    { 
        public float damage = 10;
        public float health = 100;
        public bool isOnGround;
        public bool hasCollided;
        public float decayTickDamage = 1;

        public Poultry ToPoultry(IDestructionProvider destructionProvider)
        {
            return new Poultry(decayTickDamage, damage, health, isOnGround, hasCollided, destructionProvider);
        }

    }
}