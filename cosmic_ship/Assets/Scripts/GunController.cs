using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    private bool m_isActive = false;
    public GameObject XROrigin;
    public GameObject PlayerOrigin;
    public Transform Body;
    public Transform Guns;
    public Transform ForwardSource;
    public XRController controller;
    public GameObject BulletPrefab;
    public float BulletSpeed = 5f;
    public AudioSource Audio;

    public void Activate()
    {
        controller.enabled = true;
        XROrigin.SetActive(true);
        PlayerOrigin.SetActive(false);
        m_isActive = true;
        Debug.Log("activate");
    }


    public void Deactivate()
    {
        controller.enabled = false;
        XROrigin.SetActive(false);
        PlayerOrigin.SetActive(true);
        m_isActive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Deactivate();
    }
    
    public void Shoot()
    {
        Audio.Play();
        var projectile = Instantiate(BulletPrefab, Guns.position, Quaternion.Euler(Guns.eulerAngles));
        projectile.GetComponent<Rigidbody>().velocity = -(Guns.up).normalized * BulletSpeed;
 
    }

    // Update is called once per frame
    void Update()
    {
        bool buttonPressed;
        controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonPressed);

        if (buttonPressed)
        {
            Deactivate();
        }    
        if (m_isActive)
        {
            Body.eulerAngles = new Vector3(Body.eulerAngles.x, ForwardSource.eulerAngles.y, Body.eulerAngles.z);
            Guns.localEulerAngles = new Vector3(ForwardSource.localEulerAngles.x, Guns.localEulerAngles.y, Guns.localEulerAngles.z);
        }
    }
}
