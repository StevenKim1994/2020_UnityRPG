using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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


    [SerializeField] 
    private Button[] menuBtn;


    // Start is called before the first frame update
    void Start()
    {
        
        uiPosition = GetComponent<Transform>().transform;
        endPos = uiPosition.localPosition;
        uiPosition.localPosition = startPos;

        for (int i = 0; i < menuBtn.Length; i++)
        {
            menuBtn[i].gameObject.GetComponent<Image>().color = new Vector4(255,255,255,0); // 알파값 0로 나중에 fade in 효과주기 위해
            menuBtn[i].transform.GetChild(0).GetComponent<Text>().color = new Vector4(255,255,255,0);
            menuBtn[i].gameObject.SetActive(false); // title 배너가 내려오기전까진 각버튼 비활성화
        }

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
          
            audioSource.clip = soundEffect;
            audioSource.Play();

            for (int i = 0; i < menuBtn.Length; i++)
            {
                menuBtn[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < menuBtn.Length; i++)
            {
                StartCoroutine(fadeButton(menuBtn[i], true, 1.5f)); // 각각 코루틴으로 병렬적으로 각각 코드동시 실행
            }
        }
    }

    IEnumerator fadeButton(Button button, bool fadeIn, float duration)
    {
        float counter = 0f;

        float a, b;

        if (fadeIn) // fade == true 점점 불투명하게
        {
            a = 0;
            b = 1;
        }
        else
        {
            a = 1;
            b = 0;
        }

        Image buttonImage = button.GetComponent<Image>();
        Text buttonText = button.GetComponentInChildren<Text>();

        if (!button.enabled)
            button.enabled = true;

        if (!buttonImage.enabled)
            buttonImage.enabled = true;

        if (!buttonText.enabled)
            buttonText.enabled = true;

        Color buttonColor = buttonImage.color;
        Color textColor = buttonText.color;

        ColorBlock colorBlock = button.colors;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            //Debug.Log(alpha);

            if (button.transition == Selectable.Transition.None || button.transition == Selectable.Transition.ColorTint)
            {
                buttonImage.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, alpha);//Fade Traget Image
                buttonText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);//Fade Text
            }
            else if (button.transition == Selectable.Transition.SpriteSwap)
            {
                ////Fade All Transition Images
                colorBlock.normalColor = new Color(colorBlock.normalColor.r, colorBlock.normalColor.g, colorBlock.normalColor.b, alpha);
                colorBlock.pressedColor = new Color(colorBlock.pressedColor.r, colorBlock.pressedColor.g, colorBlock.pressedColor.b, alpha);
                colorBlock.highlightedColor = new Color(colorBlock.highlightedColor.r, colorBlock.highlightedColor.g, colorBlock.highlightedColor.b, alpha);
                colorBlock.disabledColor = new Color(colorBlock.disabledColor.r, colorBlock.disabledColor.g, colorBlock.disabledColor.b, alpha);

                button.colors = colorBlock; //Assign the colors back to the Button
                buttonImage.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, alpha);//Fade Traget Image
                buttonText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);//Fade Text
            }
            else
            {
                Debug.LogError("Button Transition Type not Supported");
            }

            yield return null;
        }

        if (!fadeIn)
        {
            //Disable both Button, Image and Text components
            buttonImage.enabled = false;
            buttonText.enabled = false;
            button.enabled = false;
        }

    }
    
}
