using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ConstructableGunController : MonoBehaviour
{
    public SmartSocket BulletSocket;

    public GameObject Indicator;
    public GameObject RedLight;
    public GameObject GreenLight;

    public Material powered;

    public Material unpowered;

    public Animator BoxGunAnimator;

    private BulletManager Bullet;

    public SmartSocket SpacegunD1Socket;
    public SmartSocket SpacegunD2Socket;
    public SmartSocket SpacegunD3Socket1;
    public SmartSocket SpacegunD3Socket2;

    public GameObject SpacegunPartD2;
    public GameObject SpacegunPartD3;
    public GameObject SpacegunPartD4;
    public GameObject SpacegunPartBarrel;


    public Transform SpaceGunD2;
   
    public Transform ForwardSource;
    public XRController Controller;
    public GameObject BulletPrefab;
    public AudioSource Audio;
    public float BulletSpeed = 5f;
    public GameObject PlayerOrigin;
    public GameObject XROrigin;
    public Text aim;
    

    private bool ready = false;
    private bool m_isActive = false;

    // Update is called one per frame

    public void ToogleBatterySocket()
    {
        
        if (BoxGunAnimator.GetBool("GunSwitch"))
            CloseSocket();
        else
            OpenSocket();
        
    }

    void Start()
    {
       
        Deactivate();
    }

    private void Update()
    {
        if (SpacegunD1Socket.selectedObject != null && SpacegunD2Socket.selectedObject != null && SpacegunD3Socket1.selectedObject != null && SpacegunD3Socket2.selectedObject != null &&
            Bullet != null &&
            Bullet.charged != 0 &&
            !BoxGunAnimator.GetBool("GunSwitch")
            )
        {
            
            ready = true;
        }
        else
        {
           ready = false;
        }

        if (m_isActive)
        {
            SpaceGunD2.rotation = Quaternion.LookRotation(ForwardSource.up, ForwardSource.forward);
            RaycastHit hit;
            Physics.Raycast(SpaceGunD2.position + SpaceGunD2.up * 5, SpaceGunD2.up, out hit, 500f);
            if (hit.collider != null)
                aim.color = Color.green;
            else
                aim.color = Color.red;
                
            
        }
            

        bool buttonPressed;
        Controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out buttonPressed);

        if (buttonPressed)
        {
            Deactivate();
        }

    }



    public void OpenSocket()
    {
        Debug.Log("OPen");
        BoxGunAnimator.SetBool("GunSwitch", true);
        Indicator.GetComponent<Renderer>().material = unpowered;
        RedLight.SetActive(true);
        GreenLight.SetActive(false);
    }
    
    public void CloseSocket()
    {
        BoxGunAnimator.SetBool("GunSwitch", false);
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

    public void Activate()
    {
        if (!ready) return;
        Controller.enabled = true;
        XROrigin.SetActive(true);
        PlayerOrigin.SetActive(false);
        m_isActive = true;
        Debug.Log("activate");
    }

    public void Deactivate()
    {
        Controller.enabled = false;
        XROrigin.SetActive(false);
        PlayerOrigin.SetActive(true);
        m_isActive = false;
    }

    public void Shoot()
    {
        if (!ready) return;
        Bullet.Discharge(20);
        Audio.Play();
        var projectile = GameObject.Instantiate(BulletPrefab, SpaceGunD2.position, Quaternion.LookRotation(SpaceGunD2.up, SpaceGunD2.forward));
        projectile.GetComponent<Rigidbody>().velocity = SpaceGunD2.up.normalized * BulletSpeed;

    }
}
