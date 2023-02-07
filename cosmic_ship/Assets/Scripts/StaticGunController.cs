using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGunController : MonoBehaviour
{
    public SmartSocket BulletSocket;

    public GameObject Indicator;
    public GameObject RedLight;
    public GameObject GreenLight;


    public Material powered;

    public Material unpowered;

    public Animator BoxAnimator;

    private BulletManager Bullet;
    public GameObject BulletPrefab;
    public AudioSource Audio;
    public Transform shootTransform;
    public float BulletSpeed = 5f;
    public void ToogleBatterySocket()
    {

        if (BoxAnimator.GetBool("Button"))
            CloseSocket();
        else
            OpenSocket();

    }



    public void OpenSocket()
    {
        BoxAnimator.SetBool("Button", true);
        Indicator.GetComponent<Renderer>().material = unpowered;
        RedLight.SetActive(true);
        GreenLight.SetActive(false);
    }

    public void CloseSocket()
    {
        BoxAnimator.SetBool("Button", false);
        Debug.Log("close");
        Debug.Log(BulletSocket.selectedObject);
        if (BulletSocket.selectedObject != null)
        {
            Bullet = BulletSocket.selectedObject.GetComponent<BulletManager>();
            Debug.Log(Bullet);
            if (Bullet != null && Bullet.charged != 0)
            {
                Indicator.GetComponent<Renderer>().material = powered;
                RedLight.SetActive(false);
                GreenLight.SetActive(true);
            }
            else
            {
                Indicator.GetComponent<Renderer>().material = unpowered;
                RedLight.SetActive(true);
                GreenLight.SetActive(false);
            }
        }
        else
        {
            Indicator.GetComponent<Renderer>().material = unpowered;
            RedLight.SetActive(true);
            GreenLight.SetActive(false);
        }

    }
    

    public void Shoot()
    {
        if (Bullet == null || Bullet.charged == 0)
            return;
        Bullet.Discharge(20);
        if (Bullet.charged == 0)
        {
            Indicator.GetComponent<Renderer>().material = unpowered;
            RedLight.SetActive(true);
            GreenLight.SetActive(false);
        }
        Audio.Play();
        var projectile = GameObject.Instantiate(BulletPrefab, shootTransform.position, Quaternion.Euler(shootTransform.eulerAngles));
        projectile.GetComponent<Rigidbody>().velocity = (shootTransform.forward).normalized * BulletSpeed;
    }

}
