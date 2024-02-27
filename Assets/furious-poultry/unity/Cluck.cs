using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class Cluck : Poultry
    {
        public override bool IsDead()
        {
            return isOnGround;
        }

        public override void DoPrimaryAbility()
        {
            Debug.Log("cluck doesnt have an ability");
        }
    }
}