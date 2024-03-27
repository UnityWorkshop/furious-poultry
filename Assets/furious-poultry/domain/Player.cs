namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class Player
    {
        private ClampableIndex _currentPrefabIndex;
        public int CurrentIndex => _currentPrefabIndex.Index;

        public Player(int poultryCount)
        {
            _currentPrefabIndex = new ClampableIndex(0 , 0, poultryCount -1);
        }
        
        public void NextPoultry()
        {
            _currentPrefabIndex.IncrementIndex();
        }

        public void PreviousPoultry()
        {
            _currentPrefabIndex.DecrementIndex();
        }
    }
}