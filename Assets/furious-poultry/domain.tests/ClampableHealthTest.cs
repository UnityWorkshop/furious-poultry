using com.github.UnityWorkshop.furious_poultry.domain;
using NUnit.Framework;

namespace furious_poultry.unity.definition.domain.tests
{
    public class ClampableHealthTest
    {
        [Test]
        public void DecreaseHealth_ReduceHealthByOne()
        {
            var clampableHealth = new ClampableHealth(100, 0, 100);

            clampableHealth.DecreaseHealth(1);
            
            Assert.AreEqual(99, clampableHealth.Health);
        }
        
        [Test]
        public void DecreaseHealth_ReduceHealthByOneTwice()
        {
            var clampableHealth = new ClampableHealth(100, 0, 100);

            clampableHealth.DecreaseHealth(1);
            clampableHealth.DecreaseHealth(1);
            
            Assert.AreEqual(98, clampableHealth.Health);
        }

        [Test]
        public void DecreaseHealth_ClampedAtMin()
        {
            var clampableHealth = new ClampableHealth(1, 0, 100);

            clampableHealth.DecreaseHealth(100);
            
            Assert.AreEqual(0, clampableHealth.Health);
        }

        [Test]
        public void IncreaseHealth_HealByOne()
        {
            var clampableHealth = new ClampableHealth(50, 0, 100);

            clampableHealth.IncreaseHealth(1);
            
            Assert.AreEqual(51, clampableHealth.Health);
        }
        
        [Test]
        public void IncreaseHealth_HealByOneTwice()
        {
            var clampableHealth = new ClampableHealth(50, 0, 100);

            clampableHealth.IncreaseHealth(1);
            clampableHealth.IncreaseHealth(1);
            
            Assert.AreEqual(52, clampableHealth.Health);
        }
        
        [Test]
        public void IncreaseHealth_ClampedAtMax()
        {
            var clampableHealth = new ClampableHealth(100, 0, 100);

            clampableHealth.IncreaseHealth(100);
            
            Assert.AreEqual(100, clampableHealth.Health);
        }
        
        
        
        
    }
}