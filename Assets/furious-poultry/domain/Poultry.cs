
namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Poultry
    {
        public float Damage { get; }
        public float Health { get; private set; }
        public bool IsOnGround { get; private set; }
        public bool HasCollided { get; private set; }
        public float DecayTickDamage { get; }
        private IDestructionProvider _destructionProvider;

        public Poultry(float decayTickDamage, float damage, float health, bool isOnGround, bool hasCollided, IDestructionProvider destructionProvider)
        {
            this.DecayTickDamage = decayTickDamage;
            this.Damage = damage;
            this.Health = health;
            this.IsOnGround = isOnGround;
            this.HasCollided = hasCollided;
            _destructionProvider = destructionProvider;
        }

        public void Tick()
        {
            if (HasCollided)
            {
                Harm(DecayTickDamage);
            }

            if (Health <= 0)
            {
                _destructionProvider.Destruct();
            }
        }

        public void Harm(float damage)
        {
            Health -= damage;
        }

        public void CollidedWithEnemy(Enemies enemy)
        {
            enemy.Harm(Damage);
            Harm(Damage);
            HasCollided = true;
        }

        public void CollidedWithGround()
        {
            IsOnGround = true;
            _destructionProvider.Destruct();
        }
        
        
    }
}