using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class ZeplinAgent : MonoBehaviour
{
    
    [SerializeField] private float stoppingDistance;
    [SerializeField] private List<Path> paths;
    
    private PathManager _manager;
    
    private NavMeshAgent _navMeshAgent;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _manager = new PathManager(paths);
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAgentAtTarget())
            SetNewDestination();
        
        

        if (Input.GetKeyDown(KeyCode.A))
            _manager.StartToChangePaths();
    }
    

    bool IsAgentAtTarget()
    {
        return Vector3.Distance(transform.position, _navMeshAgent.destination) <= stoppingDistance;
    }

    public void SetNewDestination()
    {
        _navMeshAgent.destination = _manager.GetNewTarget().position;
    }
    
}
