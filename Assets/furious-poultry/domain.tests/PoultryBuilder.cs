using System.Security.Cryptography.X509Certificates;
using com.github.UnityWorkshop.furious_poultry.domain;

namespace furious_poultry.domain.tests
{
    public class PoultryBuilder
    {
        private float _decayTickDamage = 1; 
        private float _damage = 10;
        private float _health = 100;
        private bool _hasCollided = false;
        
        public Poultry Build()
        {
            return new Poultry(_decayTickDamage, _damage, _health, _hasCollided);
        }

        public PoultryBuilder UseHealth(float health)
        {
            _health = health;
            return this;
        }

        public PoultryBuilder UseDamage(float damage)
        {
            _damage = damage;
            return this;
        }
        
        public PoultryBuilder UseDecayTickDamage(float damage)
        {
            _decayTickDamage = damage;
            return this;
        }
        
        public PoultryBuilder HasCollided()
        {
            _hasCollided = true;
            return this;
        }
    }
}