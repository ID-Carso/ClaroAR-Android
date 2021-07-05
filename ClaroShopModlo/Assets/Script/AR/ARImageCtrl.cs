using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//http://objects-us-east-1.dream.io/arassetsinfinitum/android/claroshopscan
// http://objects-us-east-1.dream.io/arassetsinfinitum/android/ClaroShopIconBox.jpg
public class ARImageCtrl : MonoBehaviour
{
    private ARTrackedImageManager aRTrackedImageManager;
    private GameObject ScanContent;
    private Texture2D ScanImage;

    private GameObject ImageARContent;

    private bool HasDownloadContent;


    public string GOLink = "http://objects-us-east-1.dream.io/arassetsinfinitum/android/claroshopscan";
    public string TExtureLink = "http://objects-us-east-1.dream.io/arassetsinfinitum/android/ClaroShopIconBox.jpg";

    public GameObject SearchingGO;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     *  private void OnEnable()
     {
         aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
     }

     private void OnDisable()
     {
         aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;        
     }
     */

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            switch (trackedImage.referenceImage.name)
            {

                case "Box":
                    SearchingGO.SetActive(false);
                   ImageARContent = Instantiate(ScanContent, trackedImage.transform.position, trackedImage.transform.rotation);
                   ImageARContent.SetActive(true);
                   break;

            }
        }


        foreach (ARTrackedImage trackedImage in args.updated)
        {
            // image is tracking or tracking with limited state, show visuals and update it's position and rotation
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                switch (trackedImage.referenceImage.name)
                {
                    case "Box":
                        ImageARContent.SetActive(true);
                        SearchingGO.SetActive(false);
                        ImageARContent.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
                        break;

                    
                }
            }
           

        }

    }


    public void EnableTrackingImage() {
        if (aRTrackedImageManager != null)
        {
            aRTrackedImageManager.enabled = true;
            UICtrl.uICtrl.HideLoadingFileCanvas();
        }
        else {
            Debug.Log("DEBUG:  Start download content");
            ContentLoader.contentLoader.StartGODownload(GOLink, DownloadModelPrefab);
            ContentLoader.contentLoader.StartTextureDownload(TExtureLink, DownloadTexturePrefab);
        }
    }


    public void DownloadModelPrefab(GameObject gameObject) {
        ScanContent = gameObject;
        InitialiceARImageTrackingManager();
    }

    public void DownloadTexturePrefab(Texture2D texture) {
        ScanImage = texture;
        InitialiceARImageTrackingManager();
    }
    
    public void InitialiceARImageTrackingManager() {

        if (ScanContent != null && ScanImage != null)
        {
            aRTrackedImageManager = gameObject.AddComponent<ARTrackedImageManager>();
            AddImage(ScanImage);
            aRTrackedImageManager.requestedMaxNumberOfMovingImages = 1;
            aRTrackedImageManager.enabled = true;
            //aRTrackedImageManager.trackedImagePrefab = ScanContent;
            aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
            //trackImageManager.trackedImagePrefab = placedObject;
            UICtrl.uICtrl.HideLoadingFileCanvas();
        }
        
    }

    void AddImage(Texture2D imageToAdd)
    {
        var library = aRTrackedImageManager.CreateRuntimeLibrary();
        if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            mutableLibrary.ScheduleAddImageWithValidationJob(
                imageToAdd,
                "Box",
                0.5f);
        }
        aRTrackedImageManager.referenceLibrary = library;
    }

    public void DisableImageTracking() {
        if (aRTrackedImageManager != null) {

            if (ImageARContent != null) {
                ImageARContent.SetActive(false);
            }
            aRTrackedImageManager.enabled = false;
        }
    }
}
