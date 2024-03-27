using System;
using System.Collections.Generic;
using System.Linq;
using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.domain.aggregates;
using com.github.UnityWorkshop.furious_poultry.domain.interfaces;
using com.github.UnityWorkshop.furious_poultry.unity.definition;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZeplinAgent : MonoBehaviour, INavigationProvider
    {
        [SerializeField] float stoppingDistance;
        [SerializeField] List<PathDefinition> paths;
        
        NavMeshAgent _navMeshAgent;
        PathManager _pathManager;
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _pathManager = new PathManager(paths, stoppingDistance, this);
        }

        // Update is called once per frame
        void Update()
        {
            _pathManager.Update(transform.position);
            
            if (Input.GetKeyDown(KeyCode.P))
                _pathManager.EnableChangingPaths();
        }

        public void SetDestination(System.Numerics.Vector3 destination)
        {
            _navMeshAgent.destination = destination.ToUnity();
        }
    }
}
