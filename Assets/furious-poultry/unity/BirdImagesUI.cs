using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BirdImagesUI : MonoBehaviour
{

    public Sprite beagle;

    public Sprite cluck;

    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void ActivateBeagle()
    {
        image.sprite = beagle;
    }
    
    public void ActivateCluck()
    {
        image.sprite = cluck;
    }
}
