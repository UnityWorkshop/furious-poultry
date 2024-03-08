using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using furious_poultry.domain;
using furious_poultry.unity;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZeplinAgent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private PathManager _pathManager;
        private Transform currentTarget;
        [SerializeField] private float stoppingDistance;
        [FormerlySerializedAs("targets")] [SerializeField] private List<IPath> paths;
        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (!paths.Any())
                throw new ArgumentException("you stupid");
            _pathManager = new PathManager(paths);
            ChangeTarget();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, currentTarget.position )<= stoppingDistance)
                ChangeTarget();
            
            if (Input.GetKeyDown(KeyCode.A))
                _pathManager.StartToChangePaths();
        }

        void ChangeTarget()
        {
            currentTarget = _pathManager.GetNewTarget();
            _navMeshAgent.destination = currentTarget.position;
        }
    }
}
