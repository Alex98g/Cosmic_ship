using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundManager : MonoBehaviour
{
    AudioSource m_AudioSource;
    Animator animator;
    public AudioClip OpenSound;
    public AudioClip ColseSound;

    private bool isOpen = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool animatorOpen = animator.GetBool("Door1");
        if (isOpen != animatorOpen)
        {
            isOpen = animatorOpen;
            if(isOpen)
            {
                m_AudioSource.clip = OpenSound; 
            }
            m_AudioSource.Play();
        }
    }
}
