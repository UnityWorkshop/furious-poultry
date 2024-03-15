using com.github.UnityWorkshop.furious_poultry.domain;
using NUnit.Framework;

namespace furious_poultry.domain.tests
{
    public class ClampableIndexTest
    {
        [Test]
        public void Increment_IncrementsByOne()
        {
            var clampableIndex = new ClampableIndex(4,0,5);
            
            clampableIndex.IncrementIndex();
            
            Assert.AreEqual(5,clampableIndex.Index);
        }
        [Test]
        public void Increment_IsClampedAtMax()
        {
            var clampableIndex = new ClampableIndex(5,0,5);
            
            clampableIndex.IncrementIndex();
            
            Assert.AreEqual(5,clampableIndex.Index);
        }
        [Test]
        public void Decrement_DecrementsByOne()
        {
            var clampableIndex = new ClampableIndex(4,0,5);
            
            clampableIndex.DecrementIndex();
            
            Assert.AreEqual(3,clampableIndex.Index);
        }
        [Test]
        public void Decrement_IsClampedAtMinimum()
        {
            var clampableIndex = new ClampableIndex(0,0,5);
            
            clampableIndex.DecrementIndex();
            
            Assert.AreEqual(0,clampableIndex.Index);
        }
    }
}