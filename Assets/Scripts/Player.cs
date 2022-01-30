using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Gun myGun;
    [SerializeField] Image myReticle;
    [SerializeField] GameManager myGameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If you hit the Escape key you go to the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            myGameManager.LoadPauseMenu();
        }

    }

    public void GetGun(bool hasGun)
    {
        myGun.gameObject.SetActive(hasGun);
        myReticle.gameObject.SetActive(hasGun);
    }

}
