using System;
using System.Collections.Generic;
using System.Linq;
using com.github.UnityWorkshop.furious_poultry.domain.entities;
using com.github.UnityWorkshop.furious_poultry.domain.interfaces;
using com.github.UnityWorkshop.furious_poultry.unity;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.domain.aggregates
{
    public class PathManager
    {
        Path nextPath;
        CyclicList<CyclicList<Transform>> _paths;
        bool changingPaths;
        Transform currentTarget;
        int currentTargetIndex;
        float _stoppingDistance;

        INavigationProvider _navigationProvider;

        public PathManager(IEnumerable<IPath> paths, float stoppingDistance, INavigationProvider navigationProvider)
        {
            _stoppingDistance = stoppingDistance;
            _navigationProvider = navigationProvider;
            IEnumerable<IPath> cachedPaths = paths.ToList();
            if (!cachedPaths.Any())
                throw new ArgumentException("you stupid");
            _paths = new CyclicList<CyclicList<Transform>>(cachedPaths.Select(x=>new CyclicList<Transform>(x.Targets)));
            Initialize();
        }
        
        public void Update(Vector3 currentLocation)
        {
            CyclicList<Transform> currentPath = _paths.GetCurrent();
            if (Vector3.Distance(currentLocation, currentPath.GetCurrent().position )<= _stoppingDistance)
                SelectNewTarget();
        }
        
        void Initialize()
        {
            CyclicList<Transform> currentPath = _paths.GetCurrent();
            Transform currentTarget = currentPath.GetCurrent();
            _navigationProvider.SetDestination(currentTarget.position.ToSystem());
        }
        
        void SelectNewTarget()
        {
            CyclicList<Transform> currentPath = _paths.GetCurrent();
            currentPath.GoToNext();
            _navigationProvider.SetDestination(currentPath.GetCurrent().position.ToSystem());
        }

        public void EnableChangingPaths()
        {
            changingPaths = true;
        }
    }
}