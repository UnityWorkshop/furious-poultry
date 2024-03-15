using com.github.UnityWorkshop.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.domain;

namespace com.github.UnityWorkshop.furious_poultry.application.services
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

        public void IncomingCollision()
        {
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
            _destructionProvider.Destruct();
            _poultry.Destructed = true;
        }
        
        public void CollidedWithGround()
        {
            _poultry.IsOnGround = true;
            _destructionProvider.Destruct();
        }
    }
}