using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int charged = 100;
    public void Discharge(int value)
    {
        
        charged -= value;
        if(charged < 0)
            charged = 0;
        
        GetComponent<Renderer>().material.SetFloat("_Charge", (float)charged / 100f);
    }

    public void Charge()
    {
        charged = 100;
        GetComponent<Renderer>().material.SetFloat("_Charge", (float)charged / 100f);
        // GetComponent<Renderer>().material.SetFloat("Charge", (float)charged / 100);
    }

    public void Start()
    {
        GetComponent<Renderer>().material.SetFloat("_Charge", (float)charged / 100f);
    }
}
