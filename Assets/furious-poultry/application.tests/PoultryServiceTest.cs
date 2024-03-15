using com.github.UnityWorkshop.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.application.services;
using com.github.UnityWorkshop.furious_poultry.domain;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace furious_poultry.application.tests
{
    public class PoultryServiceTest
    {
        [Test]
        public void CollidedWithEnemy_NoDestroy_WhenNoDed()
        {
            var poultry = new Poultry(1, 10, 11, false, false);
            var warthog = new Warthog(101234);

            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            PoultryService poultryService = new PoultryService(destructionProvider, poultry);
            
            poultryService.CollidedWithEnemy(warthog);
            
            destructionProvider.DidNotReceive().Destruct();
        }
        
        [Test]
        public void CollidedWithEnemy_Destroy_WhenDed()
        {
            var poultry = new Poultry(1, 10, 10, false, false);
            var warthog = new Warthog(101234);

            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            PoultryService poultryService = new PoultryService(destructionProvider, poultry);
            
            poultryService.CollidedWithEnemy(warthog);
            
            destructionProvider.Received().Destruct();
        }
    }
}