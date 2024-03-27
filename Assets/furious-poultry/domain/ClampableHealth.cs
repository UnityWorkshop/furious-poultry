using System;

namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class ClampableHealth
    {
        public float Health; 
        private float _minimumHealth;
        private float _maximumHealth;

        public ClampableHealth(float currentHealth, float minimumHealth, float maximumHealth)
        {
            Health = currentHealth;
            _minimumHealth = minimumHealth;
            _maximumHealth = maximumHealth;

        }

        private float HealthClamp(float healthChange)
        {
            return Health = Math.Clamp(Health +healthChange, _minimumHealth, _maximumHealth);
        }

        public float DecreaseHealth(float damage)
        {
            return HealthClamp(-damage);
        }

        public float IncreaseHealth(int heal)
        {
            return HealthClamp(heal);
        }
    }
}