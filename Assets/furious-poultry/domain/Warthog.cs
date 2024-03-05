using System;

namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Warthog
    {
        public float Health { get; private set; }

        public Warthog(float health)
        {
            this.Health = health;
        }

        public void Damage(float damage)
        {
            Health = Math.Clamp(Health - damage, 0, Health);
        }

        public void Kill()
        {
            Damage(Health);
        }
        
        public bool IsDead => Health <= 0;

        public void Landed()
        {
            Kill();
        }
    }
}