using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void LoadTitlePage(string TitlePage)
    {
        SceneManager.LoadScene(TitlePage);
    }


    public static void LoadMainScene(string MainScene)
    {
        SceneManager.LoadScene(MainScene);
    }


    public static void LoadMainMenu(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
    }


    public static void LoadHowToPlayPage(string HowToPlayPage)
    {
        SceneManager.LoadScene(HowToPlayPage);
    }


    public static void LoadCreditsPage(string CreditsPage)
    {
        SceneManager.LoadScene(CreditsPage);
    }



    public static void CloseGame()
    {
        Application.Quit();
    }
}
