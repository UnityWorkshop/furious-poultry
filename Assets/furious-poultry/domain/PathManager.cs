using System;
using System.Collections.Generic;

using System.Linq;
using furious_poultry.domain;
using UnityEngine;

namespace DefaultNamespace
{
    public class PathManager
    {
        

        private Path nextPath;
        private CyclicList<IPath> _paths;
        private int currentPathIndex;
        private bool changingPaths;
        private Transform currentTarget;
        private int currentTargetIndex;

        public PathManager(List<IPath> paths)
        {
            if (!paths.Any())
                throw new ArgumentException("you stupid");
            _paths = new CyclicList<IPath>(paths);
            changingPaths = false;
            currentPathIndex = 0;
        }
        
        
        public void StartToChangePaths()
        {
            changingPaths = true;
        }
        
        void TryToChangePaths(Transform currentTarget)
        {
            if (nextPath.targets.TryGetElementIndex(this.currentTarget, out int index))
            {
                ChangePath();
                currentTargetIndex = index;
            }
        }

        void ChangePath()
        {
            _paths.GoToNext();
            
            changingPaths = false;
        }
        
        public Transform GetNewTarget()
        {
            TryToChangePaths(currentTarget);
        
            if (currentTarget is null || currentTargetIndex>=_paths.GetCurrent().Targets.Count)
            {
                currentTarget = _paths.GetCurrent().Targets[0];
                currentTargetIndex = 0;
            }
            else
                currentTarget = _paths.GetCurrent().Targets[currentTargetIndex ++];

            return currentTarget;
        }
    }
}