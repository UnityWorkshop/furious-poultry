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

    private bool changingPaths;
    private int ChangePathAt;
    private int ChangePathAt2;
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
        if (IsAgentAtTarget())
        {
            CheckPathChange();
            ChangeTarget();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartToChangePaths();
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

    List<Transform> nextPath()
    {
        List<Transform> nextPath;
        if (currentPath is null || currentPathIndex>=paths.Count)
        {
            nextPath = paths[0].targets;
        }
        else
            nextPath = paths[currentPathIndex+1].targets;

        return nextPath;
    }

    void StartToChangePaths()
    {
        foreach (Transform target in currentPath)
        {
            foreach (Transform node in nextPath())
            {
                if (target == node)
                {
                    ChangePathAt = currentPath.IndexOf(target);
                    ChangePathAt2 = currentPath.IndexOf(node);
                }
            }
        }

        changingPaths = true;
    }

    void CheckPathChange()
    {
        if (changingPaths)
        {
            if (ChangePathAt == currentTargetIndex)
            {
                ChangePath();
                currentTargetIndex = ChangePathAt2;
            } 
        }
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

        changingPaths = false;
    }

    bool IsAgentAtTarget()
    {
        return Vector3.Distance(transform.position, currentTarget.position) <= stoppingDistance;
    }
}
