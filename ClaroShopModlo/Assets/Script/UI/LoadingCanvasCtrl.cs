using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCanvasCtrl : MonoBehaviour
{
    [Header("Loading File Animation")]
    public RectTransform LoadingImage;
    public Sprite[] LoadingImages;
    public float AnimationDuration;

    // Start is called before the first frame update
    void Start()
    {
        AnimationCtrl.AnimateImage(LoadingImage, LoadingImages, AnimationDuration);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
