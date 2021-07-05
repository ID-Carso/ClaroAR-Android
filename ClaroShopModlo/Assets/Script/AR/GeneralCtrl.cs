using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//ReturnToVideoActivity
public class GeneralCtrl : MonoBehaviour
{
    public enum ContentType {
        Play,
        Scan,
        Know,
        Sale,
        Filter,
        Video,
        None
    }

    public static GeneralCtrl generalCtrl;

    [Header("Control variables")]
    public ContentType SelectedContent;
    public string JsonLink;
    public string JsonName;
    [Header("UI Controllers")]
    public InstructionCanvasCtrl instructionCanvasCtrl;
    //public IntroCanvasCttrl introCanvasCttrl;

    [Header("AR Objects")]
    public ARPlaneManager aRPlaneManager;
    public ARPlaneCtrl KnowARPlaneCtrl;
    public ARPlaneCtrl PlayARPlaneCtrl;
    public ARImageCtrl aRImageCtrl;

    [Header("Downloaded Info")]
    public string SalesLink;
    public string FilterLink;


    public bool IsDebugMode;
    private void Awake()
    {
        if (generalCtrl == null) {
            generalCtrl = this;
        }
    }

    public bool HasBeenCreatedContent;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;


        //Pon canvas de carga
        UICtrl.uICtrl.ShowLoadingFileCanvas();

        //Obten los archivos locales
        if (IsDebugMode)
        {
            ContentLoader.contentLoader.StartgettingJsonToSave(JsonLink, JsonName, ProcessData);
        }
        else
        {
            ProcessData();
        }
        //
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ProcessData() {
        //ContentLoader.contentLoader.StartGettingJson(Fx, ProcessContent);
        var jsonPath = FileFinder.fileFinder.GetFilePath(JsonName);
        Debug.Log("Path " + jsonPath);
        var contents = FileFinder.fileFinder.GetJsonData(jsonPath);
        ProcessContent(contents);
    }
    

    public void ProcessContent(Content[] contents) {
        foreach (Content content in contents) {
            switch (content.typeContent) {
                case "Filtros":
                    FilterLink = content.ContentLink;
                    break;

                case "Ofertas":
                    SalesLink = content.ContentLink;
                    break;

                case "Video":
                    //introCanvasCttrl.VideoLink = content.ContentLink;
                    //instructionCanvasCtrl.VideoLink = content.ContentLink;
                    break;
                case "Conocer":
                    KnowARPlaneCtrl.KnowContentLink = content.ModelLink;

                    instructionCanvasCtrl.KnowTutorialImageLink = content.TutorialImageLink;
                    instructionCanvasCtrl.KnowTutorialTextLink = content.TutorialTextLink;
                    instructionCanvasCtrl.GetKnowTutorial();
                    break;
                case "Escanear":
                    aRImageCtrl.GOLink = content.ModelLink;
                    aRImageCtrl.TExtureLink = content.ARMarket;

                    instructionCanvasCtrl.ScanTutorialImageLink = content.TutorialImageLink;
                    instructionCanvasCtrl.ScanTutorialTextLink = content.TutorialTextLink;
                    instructionCanvasCtrl.GetScanTutorial();
                    break;

                case "Jugar":
                    PlayARPlaneCtrl.KnowContentLink = content.ModelLink;
                    instructionCanvasCtrl.PlayTutorialImageLink = content.TutorialImageLink;
                    instructionCanvasCtrl.PlayTutorialTextLink = content.TutorialTextLink;
                    instructionCanvasCtrl.GetPlayTutorial();
                    break;

            }
        }
        SelectedContent = ContentType.Scan;

        
    }

    

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

}
