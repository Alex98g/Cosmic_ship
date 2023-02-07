using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationController : MonoBehaviour
{
    public XRController Controller;
    private XRRayInteractor Interactor;
    public LocomotionSystem LocomotionSystem;
    private TeleportationProvider TeleportationProvider;
    public GameObject Projectile;

    private Vector3 teleportationTarget;
    private GameObject currentProjectile;

    private bool triggerState = false;
    public void Start()
    {

        //Controller = GetComponent<XRController>();
        Interactor = GetComponent<XRRayInteractor>();
        TeleportationProvider = LocomotionSystem.GetComponent<TeleportationProvider>();
    }

    public void Update()
    {
        bool triggerPressed;
        

        Controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);
        //Debug.Log(triggerPressed);
        RaycastHit hit;
        

        if (triggerPressed != triggerState)
        {
            triggerState = triggerPressed;
            if (triggerPressed && Interactor.enabled && Interactor.TryGetCurrent3DRaycastHit(out hit))
            {
                ShotProjectile(hit.point);
            }    
        }
        
    }



    private void ShotProjectile(Vector3 target)
    {
        Interactor.enabled = false;
        Controller.SendHapticImpulse(1f, 0.08f);
        Debug.Log(transform.position);
        currentProjectile = GameObject.Instantiate(Projectile, transform.position, transform.rotation);
        currentProjectile.layer = 6;
        currentProjectile.GetComponent<Rigidbody>().velocity = transform.forward * Interactor.velocity;
        currentProjectile.GetComponent<ProjectileController>().controller = this;
        teleportationTarget = target;

        //projectile.GetComponent<Collider>()

    }

    public void OnProjectileFall()
    {
        Debug.Log(teleportationTarget);
        Interactor.enabled = true;
        Destroy(currentProjectile);
        TeleportationProvider.QueueTeleportRequest(new TeleportRequest() { destinationPosition = teleportationTarget });

    }

    public void OnProjectileDestroy()
    {
        Interactor.enabled = true;
        Destroy(currentProjectile);
    }
}
