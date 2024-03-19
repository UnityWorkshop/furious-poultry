using com.github.UnityWorkshop.furious_poultry.domain;
using NUnit.Framework;

namespace furious_poultry.domain.tests
{
    public class PoultryTest
    {
        [Test]
        public void Harm_ReceivedDamage()
        {
            var poultry = new PoultryBuilder()
                .Build();

            poultry.Harm(10);
            
            Assert.AreEqual(90,poultry.Health);
        }

        [Test]
        public void Harm_YieldsDeath()
        {
            var poultry = new PoultryBuilder()
                .Build();
            
            poultry.Harm(200);
            
            Assert.AreEqual(true, poultry.IsDead);
        }
        
        [Test]
        public void CollidedWithNotGround_CollisionBoolState()
        {
            var poultry = new PoultryBuilder()
                .Build();

            poultry.Collided();
            
            Assert.AreEqual(true, poultry.HasCollided);
        }

        [Test]
        public void CollidedWithEnemy_CollisionBoolState()
        {
            var poultry = new PoultryBuilder()
                .Build();
            var enemy = new Warthog(100);

            poultry.CollidedWithEnemy(enemy);
            
            Assert.AreEqual(true, poultry.HasCollided);
        }
        
        [Test]
        public void CollidedWithEnemy_EnemyHarmed()
        {
            var poultry = new PoultryBuilder()
                .Build();
            var enemy = new Warthog(100);

            poultry.CollidedWithEnemy(enemy);
            
            Assert.AreEqual(90, enemy.Health);
        }

        [Test]
        public void CollidedWithEnemy_PoultryHarmed()
        {
            var poultry = new PoultryBuilder()
                .Build();
            var enemy = new Warthog(100);

            poultry.CollidedWithEnemy(enemy);

            Assert.AreEqual(90, poultry.Health);
        }
        
    }
}