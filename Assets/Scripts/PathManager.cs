using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class PathManager
    {
        

        private Path nextPath;
        private CyclicList<Path> _paths;
        private int currentPathIndex;
        private bool changingPaths;
        private Transform currentTarget;
        private int currentTargetIndex;

        public PathManager(List<Path> paths)
        {
            if (!paths.Any())
                throw new ArgumentException("you stupid");
            _paths = new CyclicList<Path>(paths);
            changingPaths = false;
            currentPathIndex = 0;
        }
        
        
        public void StartToChangePaths()
        {
            changingPaths = true;
        }
        
        void TryToChangePaths(Transform currentTarget)
        {
            for (var index = 0; index < nextPath.targets.Count; index++)
            {
                var target = nextPath.targets[index];
                if (currentTarget == target)
                {
                    ChangePath();
                    currentTargetIndex = index;
                }
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
        
            if (currentTarget is null || currentTargetIndex>=_paths.GetCurrent().targets.Count)
            {
                currentTarget = _paths.GetCurrent().targets[0];
                currentTargetIndex = 0;
            }
            else
                currentTarget = _paths.GetCurrent().targets[currentTargetIndex ++];

            return currentTarget;
        }
    }
}