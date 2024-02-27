namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Warthog
    {
        private int _health;

        public Warthog(int health)
        {
            this._health = health;
        }

        public void Damage(int damage)
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