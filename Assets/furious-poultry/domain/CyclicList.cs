using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class CyclicList<T>
    {
        
        List<T> _targets;
        int _index;

        public CyclicList(List<T> targets)
        {
            this._targets = targets;
            _index = 0;
        }


        public void GoToNext()
        {
            _index = GetNextIndex();
        }

        public T GetCurrent()
        {
            return _targets[_index];
        }

        public T GetNext()
        {
            return _targets[GetNextIndex()];
        }

        int GetNextIndex()
        {
            if (_index+1 >= _targets.Count)
            {
                return 0;
            }

            return _index+1;
        }
        

        public bool TryGetElementIndex(T nextElement, out int index)
        {
            for (index = 0; index < _targets.Count; index++)
            {
                var target = _targets[index];
                if (nextElement.Equals(target))
                    return true;
                
            }

            return false;
        }

        public List<T> ToList()
        {
            return _targets.ToList();
        }
    }
}