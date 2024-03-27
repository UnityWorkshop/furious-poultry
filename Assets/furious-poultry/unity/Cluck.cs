using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class Cluck : PoultryAuthoring
    {
        public override void DoPrimaryAbility(Vector3 direction)
        {
            Debug.Log("cluck doesnt have an ability");
        }
    }
}