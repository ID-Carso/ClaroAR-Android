using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaneCtrl : MonoBehaviour
{
    public ARRaycastManager aRRaycastManager;
    public GameObject SearchingGO;
    private GameObject KnowGOContent;
    private GameObject KnowGOARInstance;

    public GeneralCtrl.ContentType ContentType;

    public string KnowContentLink;
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
        if (GeneralCtrl.generalCtrl.SelectedContent == ContentType && (!GeneralCtrl.generalCtrl.HasBeenCreatedContent) && KnowGOContent != null)
        {
            CreatreKnowGO();
        }
    }

    public void CreateKnowContent() {
        if (KnowGOContent == null)
        {
            ContentLoader.contentLoader.StartGODownload(KnowContentLink, AsingKnowGO);
        }
        else {
            UICtrl.uICtrl.HideLoadingFileCanvas();
            //CreatreKnowGO();
        }
    }

    public void AsingKnowGO(GameObject gameObject)
    {
        KnowGOContent = gameObject;
        Debug.Log("=== KNOW content Donwloaded");
        UICtrl.uICtrl.HideLoadingFileCanvas();
        //CreatreKnowGO();
    }

    public void CreatreKnowGO()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        Debug.Log("=== screen point");
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);
        Debug.Log("=== ARRaycastManager works");
        if (hits.Count > 0)
        {

            var hittpose = hits[0].pose;
            Debug.Log("=== KNOW content plane detected");
            if (KnowGOARInstance == null)
            {
                Debug.Log("=== KNOW content Created");
                KnowGOARInstance = Instantiate(KnowGOContent, new Vector3(hittpose.position.x, -0.1f, hittpose.position.z), hittpose.rotation);
                KnowGOARInstance.SetActive(true);

                //SearchingGO.SetActive(false);

            }
            else
            {
                KnowGOARInstance.SetActive(true);
                KnowGOARInstance.transform.position = new Vector3(hittpose.position.x, -0.1f, hittpose.position.z);
                //SearchingGO.SetActive(false)
            }
            SearchingGO.SetActive(false);
            GeneralCtrl.generalCtrl.HasBeenCreatedContent = true;
        }
        //UICtrl.uICtrl.HideLoadingFileCanvas();
    }

    public void HideKnowContentInstance() {
        if (KnowGOARInstance != null)
        {
            KnowGOARInstance.SetActive(false);
        }
    }
}
