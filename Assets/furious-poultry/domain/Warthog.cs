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
            _health -= damage;
        }

        public void Kill()
        {
            _health -= _health;
        }
        
        public bool IsDead => _health <= 0;
    }
}