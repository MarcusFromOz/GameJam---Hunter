using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LairTransition : MonoBehaviour
{
    [SerializeField] GameManager myGameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            myGameManager.StartTransition();
        }
    }
}
