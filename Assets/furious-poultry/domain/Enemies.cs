using UnityEngine.UI;

namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Enemies
    {
        public float Health { get; private set; }

         public Enemies(float health)
         {
             this.Health = health;
         }

         public void Harm(float damage)
         {
             Health -= damage;
         }
    }
}