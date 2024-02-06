using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class ZeplinAgent : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform currentTarget;
    private int currentTargetIndex;
    private List<Transform> path1;
    private List<Transform> path2;
    private List<Transform> currentPath;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private List<Transform> targets ;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (!targets.Any())
            throw new ArgumentException("you stupid");
        fillPaths();
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
            currentPath=changePath();
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

    List<Transform> changePath()
    {
        /*
        pathChanging = true;
        int a = 0;
        while (true)
        {
            for(int i=0; i<nodesPerPath; i++)
            {
                if (currentPath[i] == nextPath[a])
                {
                    if (currentTargetIndex-i < nodesPerPath) return currentTargetIndex-i;
                    else if(currentTargetIndex-i > nodesPerPath) return 
                    
                }
            }

            a++;
        }
        */
        if (currentPath == path1) return path2;
        else return path1;
    }

    void fillPaths()
    {
        path1.Add(targets[0]);
        path1.Add(targets[1]);
        path1.Add(targets[2]);
        path2.Add(targets[1]);
        path2.Add(targets[2]);
        path2.Add(targets[3]);
        currentPath = path2;
    }

}
