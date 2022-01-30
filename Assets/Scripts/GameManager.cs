using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource myDialogAudioSource;
    [SerializeField] AudioSource mySFXAudioSource;
    [SerializeField] AudioSource myMusicAudioSource;
    [SerializeField] AudioSource myPup1AudioSource;
    [SerializeField] AudioSource myPup2AudioSource;
    [SerializeField] AudioSource myReverbAudioSource;

    [SerializeField] AudioClip[] introClips;
    [SerializeField] AudioClip[] tutorialClips;
    [SerializeField] AudioClip dontComeBackClip;
    [SerializeField] AudioClip myPoemClip;
    [SerializeField] AudioClip[] puppyCriesClips;
    [SerializeField] AudioClip[] WolfAttacks;

    [SerializeField] AudioClip tutorialMusic;
    [SerializeField] AudioClip chaseMusic;
    [SerializeField] AudioClip lairMusic;
    [SerializeField] AudioClip transitionMusic;
    [SerializeField] AudioClip attackMusic;
    [SerializeField] AudioClip runningMusic;
    [SerializeField] AudioClip crawlingMusic;
    [SerializeField] AudioClip endingMusic;
    [SerializeField] AudioClip creditsMusic;

    [SerializeField] Transform hunterFollowCamera;
    [SerializeField] Transform thylacineFollowCamera;

    [SerializeField] GameObject myPlayer;
    [SerializeField] GameObject myThylacineFemale;
    [SerializeField] Thylacine myThylacine;

    [SerializeField] GameObject myClosingSequence;
    [SerializeField] GameObject myFinalPosition;


    public Material skyStart;
    public Material skyHunt;
    public Material skySunset;

    public GameObject PauseScreenCanvas;
    private Component myCanvas;

    void Start()
    {
        // The game is split into 6 main phases
        // A. the intro - intro method
        // B. the hunt - by picking up the gun
        // C. the lair confrontation
        // D. the transition
        // E. multiple choices leading to 3 different endings
        // F. ending

        myPlayer.tag = "Player";
        myThylacineFemale.tag = "Untagged";

        DoTheIntro();

        //Uncomment this only when you want to short circuit the transition to test it
        //StartTransition();
    }

    //private functions

    private void DoTheIntro()
    {
        //Play the Intro Audio 
        StartCoroutine(playAudioSequentially());

        //Start the music
        myMusicAudioSource.clip = tutorialMusic;
        myMusicAudioSource.Play();
    }

    IEnumerator playAudioSequentially()
    {
        yield return null;

        for (int i = 0; i < introClips.Length; i++)
        {
            myDialogAudioSource.clip = introClips[i];
            myDialogAudioSource.Play();
            while (myDialogAudioSource.isPlaying)
            {
                yield return null;
            }
            //Debug.Log("playing introClips");
        }
    }

    

    public void StartHunting()
    {
        float timer = 0.0f;
    
        // Allow a time for hunting
        //StartCoroutine(DelayCheck());

        // Has a timer so you cant be hunting for too long (5 minutes?)
        //SkyBoxChange();

        //Change to the hunting music

    }

    public void SkyBoxChange()
    {
        RenderSettings.skybox = skyHunt;

        Color myColor = new Color(173f / 255f, 107f / 255f, 20f / 255f);
        RenderSettings.fogColor = myColor;

        RenderSettings.fog = true;
        DynamicGI.UpdateEnvironment();
    }

    IEnumerator DelayCheck()
    {
    yield return new WaitForSeconds(300.0f);
    }


    public void StartTransition()
    {
        //When you enter the triggerbox at the lair
        
        SkyBoxChange();

        
        //Added reverb to this to show it is not real current dialogue
        myReverbAudioSource.clip = dontComeBackClip;
        myMusicAudioSource.PlayDelayed(2);

        TransitionIntoFemaleThylacine();

        // thylacine female moves to dead mate
        myThylacineFemale.transform.position = myThylacine.transform.position;

        // play transition music   
        //myMusicAudioSource.clip = transitionMusic;
        //myMusicAudioSource.Play();

        // puppies cry
        myPup1AudioSource.clip = puppyCriesClips[0];
        myPup1AudioSource.PlayDelayed(0.1f);

        myPup2AudioSource.clip = puppyCriesClips[1];
        myPup2AudioSource.PlayDelayed(1.0f);

        //delay (3 seconds)
        StartCoroutine(Delay());

        // she attacks
        FemaleThylacineAttacks();

        //Player falling over
        myPlayer.transform.eulerAngles = new Vector3(-75f, 0f, 30f);

        //thylacineFollowCamera.gameObject.SetActive(false);
        //hunterFollowCamera.gameObject.SetActive(true);
        
        //delay (3 seconds)
        StartCoroutine(Delay());

        SkyBoxChange2();

        myThylacineFemale.transform.LookAt(myPlayer.transform.position);

        // play the poem
        //RunEndCredits();

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
    }

    private void TransitionIntoFemaleThylacine()
    {
        //Swaps to female thylacine

        // hunter loses Player status
        myPlayer.tag = "Untagged";
        hunterFollowCamera.gameObject.SetActive(false);
        //myPlayer.GetComponent<ThirdPersonController>().enabled = false;

        // ThylacineFemale gains Player status
        myThylacineFemale.tag = "Player";
        thylacineFollowCamera.gameObject.SetActive(true);
        //myThylacineFemale.GetComponent<ThirdPersonController>().enabled = true;

        // cut scene to view from above 
        myClosingSequence.GetComponent<PlayableDirector>().Play();
    }

    public void FemaleThylacineAttacks()
    {
        // Do something - howl
        StartCoroutine(playWolfAttacksSequentially());

        // Move to player
        myThylacineFemale.transform.position = Vector3.Lerp(transform.position, myFinalPosition.transform.position,0.2f);
    }

    IEnumerator playWolfAttacksSequentially()
    {
        yield return null;

        for (int i = 0; i < WolfAttacks.Length; i++)
        {
            mySFXAudioSource.clip = WolfAttacks[i];
            mySFXAudioSource.Play();
            while (mySFXAudioSource.isPlaying)
            {
                yield return null;
            }
        }

        myDialogAudioSource.clip = myPoemClip;
        myDialogAudioSource.PlayDelayed(1.0f);

    }

    private void RunEndCredits()
    {
        //SceneManager.LoadScene("CreditsPage");
        
        //At present this just loads the credits page.
        //We could add the text of the poem as another scene
        //or run text up the page as the poem is read out.
        //An image of the text is available in the UI folder.

        //play end credits music
        myMusicAudioSource.clip = creditsMusic;
        myMusicAudioSource.Play();
        
        //Go back to the start menu
    }


    public void SkyBoxChange2()
    {
        RenderSettings.skybox = skySunset;

        Color myColor = Color.black;
        RenderSettings.fogColor = myColor;

        RenderSettings.fog = true;
        DynamicGI.UpdateEnvironment();
    }

    public void LoadPauseMenu()
    {
        //enable pause screen
        //to avoid null object errors, only disable the canvas not the whole game object
        Canvas myCanvas = PauseScreenCanvas.GetComponent<Canvas>();
        myCanvas.enabled = !myCanvas.enabled;
        myCanvas.enabled = true;
    }
   
}
