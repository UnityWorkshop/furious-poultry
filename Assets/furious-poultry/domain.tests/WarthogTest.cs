using com.github.UnityWorkshop.furious_poultry.domain;
using NUnit.Framework;

namespace furious_poultry.unity.definition.domain.tests
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

        [Test]
        public void IsDead_isActuallyDead()
        {
            Warthog warthog = new Warthog(0);
            
            Assert.True(warthog.IsDead);
        }
        
        [Test]
        public void IsAlive_isNotDead()
        {
            Warthog warthog = new Warthog(20);
            
            Assert.False(warthog.IsDead);
        }
        
        [Test]
        public void IsDead_Landed()
        {
            Warthog warthog = new Warthog(0);
            
            warthog.Landed();
            
            Assert.True(warthog.IsDead);
        }
        
        [Test]
        public void IsDead_Kill()
        {
            Warthog warthog = new Warthog(0);
            
            warthog.Kill();
            
            Assert.True(warthog.IsDead);
        }
    }
}