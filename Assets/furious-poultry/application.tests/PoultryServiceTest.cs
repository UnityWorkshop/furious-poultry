using com.github.UnityWorkshop.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.application.services;
using com.github.UnityWorkshop.furious_poultry.domain;
using furious_poultry.domain.tests;
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
            var poultry = new PoultryBuilder()
                .UseHealth(11)
                .Build();
            
            var warthog = new Warthog(101234);

            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            PoultryService poultryService = new PoultryService(destructionProvider, poultry);
            
            poultryService.CollidedWithEnemy(warthog);
            
            destructionProvider.DidNotReceive().Destruct();
        }
        
        [Test]
        public void CollidedWithEnemy_Destroy_WhenDed()
        {
            var poultry = new PoultryBuilder()
                .UseHealth(10)
                .UseDamage(10)
                .Build();
            
            var warthog = new Warthog(101234);

            IDestructionProvider destructionProvider = Substitute.For<IDestructionProvider>();
            PoultryService poultryService = new PoultryService(destructionProvider, poultry);
            
            poultryService.CollidedWithEnemy(warthog);
            
            destructionProvider.Received().Destruct();
        }
    }
}