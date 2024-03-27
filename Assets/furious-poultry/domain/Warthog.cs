using System;

namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Warthog
    {
        public float Health { get; private set; }
        private ClampableHealth _healthClamp;

        public Warthog(float health)
        {
            this.Health = health;
            _healthClamp = new ClampableHealth(Health, 0, health);
        }

        public void Damage(float damage)
        {
            Health = _healthClamp.DecreaseHealth(damage);
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