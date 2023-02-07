using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFixer : MonoBehaviour
{
    public GameObject NormalBox;
    public GameObject BrokenBox;
    public DoorOpener door;
    void Start()
    {
        BrokenBox.SetActive(false);
        NormalBox.SetActive(true);
    }

    void Update()
    {
        if(door.m_enabled)
        {
            Fix();
        }
        else
        {
            Break();
        }
    }

    public void Fix()
    {
        BrokenBox.SetActive(false);
        NormalBox.SetActive(true);
        door.Enable();
        //Debug.Log("Fix");
    }

    public void Break()
    {
        BrokenBox.SetActive(true);
        NormalBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if(collision.CompareTag("Tape"))
        {
            Fix();
        }
    }
}
