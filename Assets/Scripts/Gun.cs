using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject myPlayer;
    [SerializeField] Thylacine myThylacine;
    [SerializeField] AudioSource myDialogAudioSource;
    [SerializeField] AudioSource mySFXAudioSource;
    [SerializeField] AudioSource myThylacineAudioSource;
    [SerializeField] AudioClip[] gunshotClips;
    [SerializeField] AudioClip[] bulletHitsDirtClips;
    [SerializeField] AudioClip thylacineSnarl;
    [SerializeField] AudioClip[] hunterMissClips;
    [SerializeField] AudioClip bulletHitsThylacineClip;
    [SerializeField] AudioClip thylacineIsHitClip;
    [SerializeField] AudioClip gotItClip;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameManager myGameManager;
    [SerializeField] CanvasGroup myFadePanel;

    bool isLocked = false;

    void Update()
    {
        if (isLocked) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!myThylacine.isDead)
            {
                if (myThylacine.lastWaypoint == false)
                {
                    //Shotgun muzzle flash
                    muzzleFlash.Play();

                    //Shotgun blast-and-reload combined sound
                    int randomChoice = Random.Range(0, gunshotClips.Length);
                    //mySFXAudioSource.clip = gunshotClips[randomChoice];
                    //mySFXAudioSource.Play();
                    mySFXAudioSource.PlayOneShot(gunshotClips[randomChoice],1);

                    if (Vector3.Distance(myPlayer.transform.position, myThylacine.transform.position) < 30.0f)
                    {
                        // bullet hits the dirt or occasionally a tree
                        int randomChoice2 = Random.Range(0, bulletHitsDirtClips.Length);
                        mySFXAudioSource.clip = bulletHitsDirtClips[randomChoice2];
                        mySFXAudioSource.PlayDelayed(1.5f);

                        //the hunter says something
                        int randomChoice3 = Random.Range(0, hunterMissClips.Length);
                        myDialogAudioSource.clip = hunterMissClips[randomChoice3];
                        myDialogAudioSource.PlayDelayed(2);

                        //thylacine snarls
                        myThylacineAudioSource.clip = thylacineSnarl;
                        myThylacineAudioSource.PlayDelayed(3);

                        //thylacine runs to next waypoint
                        myThylacine.ThylacineMove();


                    }
                }
                else
                {
                    // You have entered the Lair area so you have an "InLair" tag
                    //Need to change tag when trigger lair box

                    //Shotgun muzzle flash
                    muzzleFlash.Play();

                    //Shotgun blast-and-reload combined sound
                    int randomChoice3 = Random.Range(0, gunshotClips.Length);
                    mySFXAudioSource.clip = gunshotClips[randomChoice3];
                    mySFXAudioSource.Play();

                    //Bullet hits Thylacine                    
                    myThylacineAudioSource.clip = bulletHitsThylacineClip;
                    myThylacineAudioSource.PlayDelayed(1);
                    Debug.Log("bullet hits thylacine at x time");

                    myThylacineAudioSource.clip = thylacineIsHitClip;
                    myThylacineAudioSource.PlayDelayed(1.5f);
                    Debug.Log("thylacine is hit at y time");
                    //Debug logs to try to see if first sound finishes before next sound plays

                    myDialogAudioSource.clip = gotItClip;
                    myDialogAudioSource.PlayDelayed(2.5f);
                    
                    //thylacine dies
                    myThylacine.ThylacineDie();
                }
            }
            else
            {

                muzzleFlash.Play();
                //Fade Out
                //StartCoroutine(DoFade(myFadePanel, myFadePanel.alpha, 1));
                //Unfade
                //StartCoroutine(DoFade(myFadePanel, 1, myFadePanel.alpha));
                //Swap from the hunter to the female thylacine
                //isLocked = true;
                //myGameManager.StartTransition();
            }
        }
    }

    public IEnumerator DoFade(CanvasGroup myCanvasGroup, float start, float end)
    {
        float counter = 0f;
        float duration = 10.0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            myCanvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }

}
