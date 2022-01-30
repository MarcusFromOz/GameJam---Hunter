using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolPath : MonoBehaviour
{
    int currentLocation = 0;
    [SerializeField] GameManager myGameManager;
    [SerializeField] Thylacine myThylacine;

    public Vector3 GetNextLocation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //We are at the current waypoint
            if (i == currentLocation)
            {
                //increment to the next waypoint
                currentLocation = i + 1;
            
                //if we are not at the last waypoint 
                if (currentLocation < transform.childCount)
                {
                    //Debug.Log("Current Location:" + currentLocation);
                    return transform.GetChild(currentLocation).position;
                }

                else
                {
                    // We are at the last waypoint
                    // Transition to the next phase of the game
                    myThylacine.lastWaypoint = true;

                    //myGameManager.StartTransition();

                    //Simply return the location of the last waypoint (where we are anyway)
                    return transform.GetChild(currentLocation - 1).position;
                }
            }
        }
        //This shouldn't happen, but need to return something
        return transform.position;
    }
}
