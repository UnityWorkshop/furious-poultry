using com.github.UnityWorkshop.furious_poultry.domain;
using NSubstitute;
using NUnit.Framework;


namespace furious_poultry.domain.tests
{
    public class PoultryTest
    {
        [Test]
        public void Harm_ReceivedDamage()
        {
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(1, 10, 100, false, false, destructionProvider );

            poultry.Harm(10);
            
            Assert.AreEqual(90,poultry.Health);
        }

        [Test]
        public void Tick_YieldsHealthDecay()
        {
            
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(1, 10, 100, false, true, destructionProvider );
            
            poultry.Tick();
            
            Assert.AreEqual(99,poultry.Health);
        }
        
    }
}