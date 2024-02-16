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
    
    
    private NavMeshAgent _navMeshAgent;
    private Transform currentTarget;
    private int currentTargetIndex;
    private List<Transform> currentPath;
    private int currentPathIndex;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (!paths.Any())
            throw new ArgumentException("you stupid");
        ChangePath();
        ChangeTarget();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget is null)
        {
            throw new ArgumentException("whyyyyyyyyyy");
        }
        if (Vector3.Distance(transform.position, currentTarget.position )<= stoppingDistance)
            ChangeTarget();
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangePath();
        }
    }

    void ChangeTarget()
    {
        if (currentTarget is null || currentTargetIndex>=currentPath.Count)
        {
            currentTarget = currentPath[0];
            currentTargetIndex = 0;
        }
        else
            currentTarget = currentPath[currentTargetIndex ++];
        

        _navMeshAgent.destination = currentTarget.position;
    }

    void ChangePath()
    {
        if (currentPath is null || currentPathIndex>=paths.Count)
        {
            currentPath = paths[0].targets;
            currentPathIndex = 0;
        }
        else
            currentPath = paths[currentPathIndex ++].targets;

        currentTargetIndex = 0;
        ChangeTarget();
    }
   

}
