using com.github.UnityWorkshop.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.domain;

namespace com.github.UnityWorkshop.furious_poultry.application.services
{
    public class BallYeeterService
    {
        private ClampableIndex _currentPrefabIndex;
        private BallYeeter _ballYeeter;
        private IDestructionProvider _destructionProvider;

        public BallYeeterService(int maxIndex, BallYeeter ballYeeter)
        {
            _ballYeeter = ballYeeter;
            _currentPrefabIndex = new ClampableIndex(0, 0, maxIndex);
        }

        public void NextPrefab()
        {
            _currentPrefabIndex.IncrementIndex();
        }
        
        public void PreviousPrefab()
        {
            _currentPrefabIndex.DecrementIndex();
        }
    }
}