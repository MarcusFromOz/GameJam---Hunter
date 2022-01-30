using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] PatrolPath[] patrolPathArray;
    [SerializeField] GameManager myGameManager;
   
    PatrolPath patrolPath;
    
    void Start()
    {
        //choose a random patrol path from the those available
        patrolPath = patrolPathArray[Random.Range(0, 3)];
    }

    public PatrolPath GetPatrolPath()
    {
        return patrolPath;
    }

}
