using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrl : MonoBehaviour
{
    [Header("Canvas GO")]
    public GameObject LoadingFileGO;
    public GameObject InstructionsGO;


    [Header("Rect Transform Canvas")]
    public RectTransform LoadingFileTranform;
    public RectTransform InstructionsTransform;

    [Header("Canvas Group")]
    public CanvasGroup LoadingFileCG;
    public CanvasGroup InstructionsCG;

    [Header("Canvas Ctrls")]
    public InstructionCanvasCtrl instructionCanvasCtrl;


    public static UICtrl uICtrl;
    public bool IsAnimating;
    private void Awake()
    {
        if (uICtrl == null) {
            uICtrl = this;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        IsAnimating = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLoadingFileCanvas() {
        LoadingFileGO.SetActive(true);
        AnimationCtrl.MoveCanvas(LoadingFileTranform, LoadingFileCG, Vector2.zero, 1, 0.3f);
    }

    public void HideLoadingFileCanvas() {
        AnimationCtrl.MoveCanvas(LoadingFileTranform, LoadingFileCG, new Vector2(0, -3800), 1, 0.3f, DesactiveLoadingFileCanvas);
    }

    public void DesactiveLoadingFileCanvas()
    {
        LoadingFileGO.SetActive(false);
        ShowInstructionsPanels();
    }

    public void ShowInstructionsPanels() {
        InstructionsGO.SetActive(true);
        instructionCanvasCtrl.SetupInstructions();
        AnimationCtrl.MoveCanvas(InstructionsTransform, InstructionsCG, Vector2.zero, 1, 0.3f);
    }

    public void HideInstructionsCanvas()
    {
        AnimationCtrl.MoveCanvas(InstructionsTransform, InstructionsCG, new Vector2(0, -3800), 1, 0.3f, DesactiveInstructionsCanvas);
    }

    public void DesactiveInstructionsCanvas()
    {
        InstructionsGO.SetActive(false);
        //GeneralCtrl.generalCtrl.ShowIntro();
    }

    /*
     *  public void ShowLoadingDataCanvas() {
         LoadingDataGO.SetActive(true);
         AnimationCtrl.MoveCanvas(LoadingDataTransform, LoadingDataCG, Vector2.zero, 1, 0.3f);
     }

     public void HideLoadingDataCanvas() {
         AnimationCtrl.MoveCanvas(LoadingDataTransform, LoadingDataCG, new Vector2(0, -3800), 1, 0.3f, DesactiveLoadingDataCanvas);
     }

     public void DesactiveLoadingDataCanvas() {
         LoadingDataGO.SetActive(false);
         GeneralCtrl.generalCtrl.ShowIntro();
     }

     public void ShowIntroductionCanvas()
     {
         IntroductionGO.SetActive(true);
         AnimationCtrl.MoveCanvas(IntroductionTransform, IntroductionCG, Vector2.zero, 1, 0.3f);
     }

     public void HideIntroductionCanvas()
     {
         AnimationCtrl.MoveCanvas(IntroductionTransform, IntroductionCG, new Vector2(0, -3800), 1, 0.3f, DesactiveIntroductionCanvas);
     }

     public void DesactiveIntroductionCanvas()
     {
         IntroductionGO.SetActive(false);

     }
     */

}
