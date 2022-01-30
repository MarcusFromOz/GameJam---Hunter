using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameObject[] pauseObjects;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    // Update is called once per frame
    void Update()
    {

        //uses the p or the Escape button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0f;
                AudioListener.pause = true;
                showPaused();
            }
            else if (Time.timeScale == 0f)
            {
                Debug.Log("high");
                AudioListener.pause = false;
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }

    //Reloads the Level
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            showPaused();
        }
        else if (Time.timeScale == 0f)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadScene(string MainScene)
    {
        SceneManager.LoadScene(MainScene);
    }
}
