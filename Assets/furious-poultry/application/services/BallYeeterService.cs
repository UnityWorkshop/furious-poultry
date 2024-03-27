using System.Numerics;
using com.github.UnityWorkshop.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.domain;

namespace com.github.UnityWorkshop.furious_poultry.application.services
{
    public class BallYeeterService
    {
        private ClampableIndex _currentPrefabIndex;
        private BallYeeter _ballYeeter;
        private ITransformProvider _transformProvider;

        public BallYeeterService(int maxIndex, BallYeeter ballYeeter, ITransformProvider transformProvider)
        {
            _ballYeeter = ballYeeter;
            _transformProvider = transformProvider;
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

        public Vector3 CalculatedYeetPower()
        {
            return _transformProvider.Forward * _ballYeeter.ForceValue;
        }
    }
}