using System;

namespace com.github.UnityWorkshop.furious_poultry.domain
{
    public class ClampableIndex
    {
        public int Index { get; private set; }
        private int _minimumIndex;
        private int _maximumIndex;

        public ClampableIndex(int index, int minimumIndex, int maximumIndex)
        {
            Index = index;
            _minimumIndex = minimumIndex;
            _maximumIndex = maximumIndex;

        }
        private void IndexClamp(int offset)
        {
            Index = Math.Clamp(Index + offset, _minimumIndex, _maximumIndex);
        }

        public void IncrementIndex()
        {
            IndexClamp(1);
        }
        public void DecrementIndex()
        {
            IndexClamp(-1);
        }
    }
}