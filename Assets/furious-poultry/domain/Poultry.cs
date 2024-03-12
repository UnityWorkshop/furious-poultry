
using System;

namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Poultry
    {
        public float Damage { get; }
        public float Health { get; private set; }
        public bool IsOnGround { get; set; }
        public bool HasCollided { get; private set; }
        public float DecayTickDamage { get; }
        private IDestructionProvider _destructionProvider;
        private ClampableHealth _healthClamp;
        

        public Poultry(float decayTickDamage, float damage, float health, bool isOnGround, bool hasCollided, )
        {
            this.DecayTickDamage = decayTickDamage;
            this.Damage = damage;
            this.Health = health;
            this.IsOnGround = isOnGround;
            this.HasCollided = hasCollided;
            _destructionProvider = destructionProvider;
            _healthClamp = new ClampableHealth(Health, 0, health);
        }

        public bool IsDead => Health <= 0;
        public bool Destructed;

        

        public void Harm(float damage)
        {
            Health = _healthClamp.DecreaseHealth(damage);
        }

        public void CollidedWithEnemy(Warthog enemy)
        {
            enemy.Damage(Damage);
            Harm(Damage);
            HasCollided = true;
        }

        

        public void CollidedWithNotGround()
        {
            HasCollided = true;
        }
    }
}