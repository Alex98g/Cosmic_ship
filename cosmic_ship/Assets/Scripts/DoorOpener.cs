using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    Animator animator;
    public GameObject left;
    public GameObject right;
    public AudioSource Sound_Close;
    public AudioSource Sound_Open;

    public bool m_enabled = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Enable()
    {
        left.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0f, 1f, 0) * 2f);
        right.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(0f, 1f, 0) * 2f);
        m_enabled = true;
    }

    public void Open()
    {
        animator.SetBool("Door1", true);
        Sound_Open.Play();
    } 

    public void Close()
    {
        animator.SetBool("Door1", false);
        Sound_Close.Play();
    }
    public void Disable()
    {
        left.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 0f, 0) * 2f);
        right.GetComponent<Renderer>().material.SetVector("_EmissionColor", new Vector4(1f, 0f, 0) * 2f);
        m_enabled = false;
        Close();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && m_enabled)
        {
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Close();
        }
    }
}
