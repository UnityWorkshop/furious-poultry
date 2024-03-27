namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Beagle: Poultry
    {
        public int PelletAmount { get; }
        public float PelletSpeed { get; }
        public float PelletSpread { get; }

        public Beagle(float decayTickDamage, float damage, float health, bool hasCollided, int pelletAmount, float pelletSpeed, float pelletSpread) 
            : base(decayTickDamage, damage, health, hasCollided)
        {
            PelletAmount = pelletAmount;
            PelletSpeed = pelletSpeed;
            PelletSpread = pelletSpread;
        }
    }
}