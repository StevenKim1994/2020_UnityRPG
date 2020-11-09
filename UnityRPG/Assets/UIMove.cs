using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    private Transform uiPosition;

    [SerializeField]
    private Vector3 startPos = new Vector3(0,300,0); // 시작할때 UI 시작위치
    [SerializeField]
    private Vector3 endPos; // 최종 UI 위치
    [SerializeField]
    private float moveSpeed = -0.05f;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundEffect;

    private bool ended = false;



   
    
    // Start is called before the first frame update
    void Start()
    {
        
        uiPosition = GetComponent<Transform>().transform;
        endPos = uiPosition.localPosition;
        uiPosition.localPosition = startPos;
      

    }

    // Update is called once per frame
    void Update()
    {



        uiPosition.localPosition += new Vector3(0,Mathf.Sin(Time.deltaTime) * moveSpeed ,0);

        if (uiPosition.localPosition.y <= endPos.y)
            uiPosition.localPosition = endPos;

        if (uiPosition.localPosition == endPos && ended == false)
        {
            ended = true;
            Debug.Log("UIENDED");
            audioSource.clip = soundEffect;
            audioSource.Play();
            
            
        }
    }
}
