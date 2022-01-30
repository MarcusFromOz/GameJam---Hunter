using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thylacine : MonoBehaviour
{

    [SerializeField] GameObject myPlayer;
    [SerializeField] GameManager myGameManager;
    [SerializeField] AIController myAIController;
    [SerializeField] float speed = 10.0f;
    [SerializeField] AudioSource myThylacineAudioSource;
    [SerializeField] AudioClip[] thylacineHowlClips;

    public Vector3 nextWaypoint;
    bool isWaypointSelected = false;
    public bool lastWaypoint = false;
    public bool isDead = false;

    Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }
       
    void Update()
    {
        // this only happens during the hunt phase
        if (!isDead)
        {
            if (isWaypointSelected)
            {
                // you have already selected the next waypoint, during this frame just move towards it
                transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, speed * Time.deltaTime);
                transform.LookAt(nextWaypoint);
                anim.SetBool("run", true);

                // if you are already close to the next waypoint, stay where you are
                if (Vector3.Distance(nextWaypoint, gameObject.transform.position) < 0.1f)
                {
                    isWaypointSelected = false;
                    transform.LookAt(myPlayer.transform);
                    anim.SetBool("run", false);
                }
            }

            else
            {
                // if the player gets too close to the thylacine then move to the next waypoint
                if (Vector3.Distance(myPlayer.transform.position, gameObject.transform.position) < 10.0f)
                {
                    //thylacine howls
                    int randomChoice = Random.Range(0, thylacineHowlClips.Length);
                    myThylacineAudioSource.clip = thylacineHowlClips[randomChoice];
                    myThylacineAudioSource.Play();

                    ThylacineMove();
                }
            }
        }
    }

    public void ThylacineMove()
    {
        //get the next waypoint
        nextWaypoint = myAIController.GetPatrolPath().GetNextLocation();

        isWaypointSelected = true;
    }

    public void ThylacineDie()
    {
        anim.SetBool("die", true);
        isDead = true;
    }
}
