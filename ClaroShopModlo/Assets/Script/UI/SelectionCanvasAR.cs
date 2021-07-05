using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class SelectionCanvasAR : MonoBehaviour
{
    [Header("Active Icons")]
    public GameObject PlaySelectedGO;
    public GameObject KnowSelectedGO;
    public GameObject ScanSelectedGO;
    public GameObject SalesSelectedGO;
    public GameObject FilterSelectedGO;

    [Header("AR Objects")]
    public ARPlaneManager aRPlaneManager;
    public ARPlaneCtrl KnowARPlaneCtrl;
    public ARPlaneCtrl PlayARPlaneCtrl;
    public ARImageCtrl aRImageCtrl;

    [Header("Searching Content")]
    public CanvasGroup SearchingCG;
    public GameObject SearchingGO;

    [Header("Sccore")]
    public GameObject ScoreContent;

    // Start is called before the first frame update
    void Start()
    {
        AnimationCtrl.ModifyAlphaWithLoop(SearchingCG, 0, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOnPlayBtn() {
        GeneralCtrl.generalCtrl.SelectedContent = GeneralCtrl.ContentType.Play;
        PlaySelectedGO.SetActive(true);
        KnowSelectedGO.SetActive(false);
        ScanSelectedGO.SetActive(false);
        SalesSelectedGO.SetActive(false);
        FilterSelectedGO.SetActive(false);
        SearchingGO.SetActive(true);
        UICtrl.uICtrl.ShowLoadingFileCanvas();
        aRPlaneManager.enabled = true;
        SetAllPlanesActtive(true);
        GeneralCtrl.generalCtrl.HasBeenCreatedContent = false;
        ScoreContent.SetActive(true);
        KnowARPlaneCtrl.HideKnowContentInstance();
        aRImageCtrl.DisableImageTracking();
        PlayARPlaneCtrl.HideKnowContentInstance();
        PlayARPlaneCtrl.CreateKnowContent();
       
    }

    public void ClickOnKnowBtn()
    {
        GeneralCtrl.generalCtrl.SelectedContent = GeneralCtrl.ContentType.Know;
        PlaySelectedGO.SetActive(false);
        KnowSelectedGO.SetActive(true);
        ScanSelectedGO.SetActive(false);
        SalesSelectedGO.SetActive(false);
        FilterSelectedGO.SetActive(false);
        SearchingGO.SetActive(true);
        UICtrl.uICtrl.ShowLoadingFileCanvas();
        ScoreContent.SetActive(false);
        aRPlaneManager.enabled = true;
        SetAllPlanesActtive(true);
        GeneralCtrl.generalCtrl.HasBeenCreatedContent = false;
        KnowARPlaneCtrl.HideKnowContentInstance();
        aRImageCtrl.DisableImageTracking();
        PlayARPlaneCtrl.HideKnowContentInstance();
        KnowARPlaneCtrl.CreateKnowContent();
    } 


    public void ClickOnScanBtn() {
        GeneralCtrl.generalCtrl.SelectedContent = GeneralCtrl.ContentType.Scan;
        PlaySelectedGO.SetActive(false);
        KnowSelectedGO.SetActive(false);
        ScanSelectedGO.SetActive(true);
        SalesSelectedGO.SetActive(false);
        FilterSelectedGO.SetActive(false);
        SearchingGO.SetActive(true);
        UICtrl.uICtrl.ShowLoadingFileCanvas();
        ScoreContent.SetActive(false);
        aRPlaneManager.enabled = false;
        SetAllPlanesActtive(false);
        PlayARPlaneCtrl.HideKnowContentInstance();
        KnowARPlaneCtrl.HideKnowContentInstance();
        aRImageCtrl.DisableImageTracking();
        aRImageCtrl.EnableTrackingImage();
    }

    public void ClickOnSaleBtn() {
        GeneralCtrl.generalCtrl.OpenLink(GeneralCtrl.generalCtrl.SalesLink);
        ClickOnScanBtn();
    }

    public void ClickOnFilterBtn() {
        GeneralCtrl.generalCtrl.OpenLink(GeneralCtrl.generalCtrl.FilterLink);
        ClickOnScanBtn();
    }


    public void SetAllPlanesActtive(bool isActive)
    {
        aRPlaneManager.enabled = isActive;
        foreach (var plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(isActive);
        }
    }

    public void OnBBackBtn()
    {
#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.Unity");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("ReturnToMainActivity", "Hello");
#endif

    }

    public void OnGoToVideoBtn()
    {
#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.Unity");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("ReturnToVideoActivity", "Hello");
#endif

    }

}
