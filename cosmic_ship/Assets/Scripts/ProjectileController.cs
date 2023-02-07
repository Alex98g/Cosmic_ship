using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public TeleportationController controller;
    public AudioSource sound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Teleportation"))
            Fallen();
        else
            Destroyed();
    }

    private void Fallen()
    {
        sound.Play();
        controller.SendMessage("OnProjectileFall");
        
    }

    private void Destroyed()
    {
        sound.Play();
        controller.SendMessage("OnProjectileDestroy");
       
    }
    public void Update()
    {
        if(transform.position.y <= 0)
        {
            Fallen();
        }
    }

}
