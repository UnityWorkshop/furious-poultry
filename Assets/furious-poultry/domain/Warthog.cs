using System;

namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Warthog
    {
        private float _health;

        public Warthog(float health)
        {
            this._health = health;
        }

        public void Damage(float damage)
        {
            _health = Math.Clamp(_health - damage, 0, _health);
        }

        public void Kill()
        {
            Damage(_health);
        }
        
        public bool IsDead => _health <= 0;

        public void Landed()
        {
            Kill();
        }
    }
}