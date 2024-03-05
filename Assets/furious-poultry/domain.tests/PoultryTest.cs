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

        [Test]
        public void Harm_YieldsDeath()
        {
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(1, 10, 100, false, false, destructionProvider );
            
            poultry.Harm(200);
            
            Assert.AreEqual(true, poultry.IsDead);
        }

        [Test]
        public void CollidedWithGround_IsOnGroundTrue()
        {
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(1, 10, 100, false, false, destructionProvider );
            
            poultry.CollidedWithGround();
            
            Assert.AreEqual(true, poultry.IsOnGround);
        }

        [Test]
        public void CollidedWithNotGround_CollisionBoolState()
        {
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(1, 10, 100, false, false, destructionProvider );

            poultry.CollidedWithNotGround();
            
            Assert.AreEqual(true, poultry.HasCollided);
        }

        [Test]
        public void CollidedWithEnemy_CollisionBoolState()
        {
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(1, 10, 100, false, false, destructionProvider );
            var enemy = new Warthog(100);

            poultry.CollidedWithEnemy(enemy);
            
            Assert.AreEqual(true, poultry.HasCollided);
        }
        
        [Test]
        public void CollidedWithEnemy_EnemyHarmed()
        {
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(1, 10, 100, false, false, destructionProvider );
            var enemy = new Warthog(100);

            poultry.CollidedWithEnemy(enemy);
            
            Assert.AreEqual(90, enemy.Health);
        }

        [Test]
        public void CollidedWithEnemy_PoultryHarmed()
        {
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(1, 10, 100, false, false, destructionProvider);
            var enemy = new Warthog(100);

            poultry.CollidedWithEnemy(enemy);

            Assert.AreEqual(90, poultry.Health);
        }
        
        [Test]
        public void Tick_TickYieldsDeath()
        {
            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            var poultry = new Poultry(10, 10, 1, false, true, destructionProvider);

            poultry.Tick();

            Assert.AreEqual(true, poultry.Destructed);
        }
        
    }
}