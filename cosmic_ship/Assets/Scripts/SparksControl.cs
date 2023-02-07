using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksControl : MonoBehaviour
{
    //public List<AudioClip> sounds;
    public List<AudioSource> source;
    private ParticleSystem particle;
    void Start()
    {
        //source = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
        StartCoroutine(Sparks());
    }

    IEnumerator Sparks()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (gameObject.activeSelf)
            {
                Debug.Log("Sparks");
                yield return new WaitForSeconds(Random.Range(2, 9));
                source[Random.Range(0, source.Count)].Play();
                particle.Play();
            }
                
            
            
            
            
        }

    }
}
