using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHolo : MonoBehaviour
{
    public Material shade;

    void Start()
    {
        shade.SetVector("_FresnelColor", new Vector4(0f, 1f, 0f, 1f));
    }
    
    public void level1()
    {
        shade.SetVector("_FresnelColor", new Vector4(1f, 1f, 0f, 0f));
    }

    public void level2()
    {
        shade.SetVector("_FresnelColor", new Vector4(1f, 0f, 0f, 0f));
    }
}
