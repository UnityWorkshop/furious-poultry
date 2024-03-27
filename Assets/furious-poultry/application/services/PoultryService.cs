using com.github.UnityWorkshop.Assets.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.domain;

namespace com.github.UnityWorkshop.Assets.furious_poultry.application.services
{
    public class PoultryService
    {
        private IDestructionProvider _destructionProvider;

        private Poultry _poultry;

        public PoultryService(IDestructionProvider destructionProvider, Poultry poultry)
        {
            _destructionProvider = destructionProvider;
            _poultry = poultry;
        }

        public void CollidedWithEnemy(Warthog warthog)
        {
            _poultry.CollidedWithEnemy(warthog);

            if (_poultry.IsDead)
                _destructionProvider.Destruct();
        }
        
        public void Tick()
        {
            if (_poultry.HasCollided)
            {
                _poultry.Harm(_poultry.DecayTickDamage);
            }

            if (_poultry.IsDead)
            {
                Destruct();
            }
        }
        
        private void Destruct()
        {
            _poultry.Kill();
            _destructionProvider.Destruct();
        }
        
        public void CollidedWithGround()
        {
            _poultry.Collided();
            _destructionProvider.Destruct();
        }

        public void CollidedWithThing()
        {
            _poultry.Collided();
        }
    }
}