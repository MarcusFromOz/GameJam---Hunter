using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{

    [SerializeField] GameObject myPlayer;
    [SerializeField] ParticleSystem gunParticleSystem;
    [SerializeField] GameManager myGameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Pick up the Gun
            this.gameObject.SetActive(false);

            myPlayer.GetComponent<Player>().GetGun(true);
            
            //Turn off the particle system
            gunParticleSystem.Stop();

            //gameManager Hunting phase
            myGameManager.StartHunting();

        }
    }
}
