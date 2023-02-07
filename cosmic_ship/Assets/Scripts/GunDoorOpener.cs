using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDoorOpener : MonoBehaviour
{
    Animator animator;
    public GameObject left;
    public GameObject right;
    public AudioSource Sound_Close;
    public AudioSource Sound_Open;
    
    public Animator LeverArm;
    
    

    public void ToogleLeverArm()
    {
        Debug.Log($"isUp={LeverArm.GetBool("isUp")}");
        LeverArm.SetBool("isUp", !LeverArm.GetBool("isUp"));
        if (!LeverArm.GetBool("isUp"))
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    

    public void Enable()
    {
        left.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0f, 1f, 0) * 2f);
        right.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0f, 1f, 0) * 2f);
    }

    public void Open()
    {
        Sound_Open.Play();
        Enable();
    }

    public void Close()
    {
        Sound_Close.Play();
        Disable();
    }
    public void Disable()
    {
        left.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 0f, 0) * 2f);
        right.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 0f, 0) * 2f);
    }

}
