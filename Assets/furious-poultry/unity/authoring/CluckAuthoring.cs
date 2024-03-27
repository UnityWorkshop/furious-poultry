using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity.authoring
{
    public class CluckAuthoring : PoultryAuthoring
    {

        public override void DoPrimaryAbility(Vector3 direction)
        {
            Debug.Log("cluck doesnt have an ability");
        }
    }
}