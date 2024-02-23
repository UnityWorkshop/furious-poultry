using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class PathManager
    {
        
        private Path currentPath;

        private Path nextPath;
        private List<Path> _paths;
        private int currentPathIndex;
        private bool changingPaths;
        private Transform currentTarget;
        private int currentTargetIndex;

        public PathManager(List<Path> paths)
        {
            if (!paths.Any())
                throw new ArgumentException("you stupid");
            _paths = paths;
            changingPaths = false;
            currentPath = paths[0];
            currentPathIndex = 0;
            UpdateNextPath();
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
            if (currentPath is null || currentPathIndex>= _paths.Count)
            {
                currentPath = _paths[0];
                currentPathIndex = 0;
            }
            else
                currentPath = _paths[currentPathIndex ++];
            
            UpdateNextPath();
            changingPaths = false;
        }
        
        void UpdateNextPath()
        {
            if (currentPath is null ||currentPathIndex>= _paths.Count)
            {
                nextPath = _paths[0];
            }
            else
                nextPath = _paths[currentPathIndex+1];
        }
        
        public Transform GetNewTarget()
        {
            TryToChangePaths(currentTarget);
        
            if (currentTarget is null || currentTargetIndex>=currentPath.targets.Count)
            {
                currentTarget = currentPath.targets[0];
                currentTargetIndex = 0;
            }
            else
                currentTarget = currentPath.targets[currentTargetIndex ++];

            return currentTarget;
        }
    }
}