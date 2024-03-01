using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class Cluck : PoultryAuthoring
    {
        public override bool IsDead()
        {
            return Poultry.IsOnGround;
        }

        public override void DoPrimaryAbility()
        {
            Debug.Log("cluck doesnt have an ability");
        }
    }
}