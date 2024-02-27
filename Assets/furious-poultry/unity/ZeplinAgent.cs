using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZeplinAgent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Transform currentTarget;
        private int currentTargetIndex;
        [SerializeField] private float stoppingDistance;
        [SerializeField] private List<Transform> targets ;
        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (!targets.Any())
                throw new ArgumentException("you stupid");
            ChangeTarget();
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, currentTarget.position )<= stoppingDistance)
                ChangeTarget();
        }

        void ChangeTarget()
        {
            if (currentTarget is null || currentTargetIndex>=targets.Count)
            {
                currentTarget = targets[0];
                currentTargetIndex = 0;
            }
            else
                currentTarget = targets[currentTargetIndex ++];

            _navMeshAgent.destination = currentTarget.position;
        }
    }
}
