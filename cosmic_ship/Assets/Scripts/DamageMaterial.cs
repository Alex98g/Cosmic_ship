using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMaterial : MonoBehaviour
{
    public Renderer target;
    public Material noDamage;
    public Material level1;
    public Material level2;
    public WorldGameManager manager;
    void Start()
    {
        if (target == null)
            target = GetComponent<Renderer>();
        manager = FindObjectOfType<WorldGameManager>();
        manager.damages.Add(this);
    }

    public void Level1()
    {
        target.material = level1;
    }

    public void Level2()
    {
        target.material = level2;
    }
}
