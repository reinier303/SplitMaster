using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class UIButtonR : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    private Text textComponent;
    private Image imageComponent;
    [SerializeField]
    public UnityEvent OnPointerDownEvent;
    public Material HoverMaterial, DownMaterial, BaseMaterial;
    public AudioClip HoverSound, ClickSound;
    private AudioSource audioSource;

    private bool image;

    Vector4 newColor;
    Color32 newColor2;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textComponent = GetComponent<Text>();
        image = false;
        if (textComponent == null)
        {
            imageComponent = GetComponent<Image>();
            image = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!image)
        {
            textComponent.material = DownMaterial;
        }
        else
        {
            imageComponent.material = DownMaterial;
        }
        audioSource.clip = ClickSound;
        audioSource.Play();
        OnPointerDownEvent.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!image)
        {
            textComponent.material = HoverMaterial;
        }
        else
        {
            imageComponent.material = HoverMaterial;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!image)
        {
            textComponent.material = HoverMaterial;
        }
        else
        {
            imageComponent.material = HoverMaterial;
        }
        audioSource.clip = HoverSound;
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!image)
        {
            textComponent.material = BaseMaterial;
        }
        else
        {
            imageComponent.material = BaseMaterial;
        }
    }

}
