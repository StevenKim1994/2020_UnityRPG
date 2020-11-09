using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOnSound : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mouseOverAudioClip;

    void Start()
    {
        
    }

    public void MouseEnter()
    {
        Debug.Log(this.gameObject.name);
    }

}
