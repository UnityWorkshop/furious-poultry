using com.github.UnityWorkshop.furious_poultry.domain;
using NUnit.Framework;

namespace furious_poultry.domain.tests
{
    public class WarthogTest
    {
        [Test]
        public void Damage_KillsIfNoHealth()
        {
            Warthog warthog = new Warthog(20);
            
            warthog.Damage(20);
            
            Assert.True(warthog.IsDead);
        }
    }
}