
using System;

namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Poultry
    {
        public float Damage { get; }
        public float Health { get; private set; }
        public bool HasCollided { get; private set; }
        public float DecayTickDamage { get; }
        private ClampableHealth _healthClamp;
        

        public Poultry(float decayTickDamage, float damage, float health, bool hasCollided)
        {
            this.DecayTickDamage = decayTickDamage;
            this.Damage = damage;
            this.Health = health;
            this.HasCollided = hasCollided;
            _healthClamp = new ClampableHealth(Health, 0, health);
        }

        public bool IsDead => Health <= 0;

        public void Harm(float damage)
        {
            Health = _healthClamp.DecreaseHealth(damage);
        }
        
        public void Kill()
        {
            Harm(Health);
        }

        public void CollidedWithEnemy(Warthog enemy)
        {
            enemy.Damage(Damage);
            Harm(Damage);
            HasCollided = true;
        }
        
        public void Collided()
        {
            HasCollided = true;
        }
    }
}