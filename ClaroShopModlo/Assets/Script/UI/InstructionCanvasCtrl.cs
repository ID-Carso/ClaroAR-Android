using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class InstructionCanvasCtrl : MonoBehaviour
{
    [Header ("Content")]
    public string[] PlayTutorialTextLink;
    public string[] PlayTutorialImageLink;

    public string[] ScanTutorialTextLink;
    public string[] ScanTutorialImageLink;

    public string[] KnowTutorialTextLink;
    public string[] KnowTutorialImageLink;


    public Sprite[] PlayTutorialText;
    public Sprite[] PlayTutorialImage;

    public Sprite[] ScanTutorialText;
    public Sprite[] ScanTutorialImage;

    public Sprite[] KnowTutorialText;
    public Sprite[] KnowTutorialImage;

    public Image IconImage;
    public Image InstructionImage;
    
    [Header("Video")]
    public string VideoLink;
    public VideoPlayer videoPlayer;

    [Header("Components")]
    public GameObject InstructionsGO;
    public GameObject VideoGO;

    public SelectionCanvasAR selectionCanvasAR;
    [Header("Flow Btns")]
    public Button GoBackBtn;
    public Button GoForwardBtn;
    public GameObject GoBackGO;
    public GameObject GoForwardGO;


    int PlayImageCount;
    int PlayTextCount;
    int ScanImageCount;
    int ScanTextCount;
    int KnowImageCount;
    int KnowTextCount;

    int NavigattionIndex;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += VideoHasEnded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupInstructions() {
        InstructionsGO.SetActive(true);
        VideoGO.SetActive(false);
        NavigattionIndex = 0;
        switch (GeneralCtrl.generalCtrl.SelectedContent) {
            case GeneralCtrl.ContentType.Play:
                IconImage.sprite = PlayTutorialImage[0];
                InstructionImage.sprite = PlayTutorialText[0];
                if (PlayTutorialImage.Length > 1) {
                    GoBackGO.SetActive(true);
                    GoForwardGO.SetActive(true);
                    GoBackBtn.interactable = false;
                    GoForwardBtn.interactable = true;
                }
                else
                {
                    GoBackGO.SetActive(false);
                    GoForwardGO.SetActive(false);
                }
                break;
            case GeneralCtrl.ContentType.Know:
                IconImage.sprite = KnowTutorialImage[0];
                InstructionImage.sprite = KnowTutorialText[0];
                if (KnowTutorialImage.Length > 1)
                {
                    GoBackGO.SetActive(true);
                    GoForwardGO.SetActive(true);
                    GoBackBtn.interactable = false;
                    GoForwardBtn.interactable = true;
                }
                else
                {
                    GoBackGO.SetActive(false);
                    GoForwardGO.SetActive(false);
                }
                break;
            case GeneralCtrl.ContentType.Scan:
                IconImage.sprite = ScanTutorialImage[0];
                InstructionImage.sprite = ScanTutorialText[0];
                if (ScanTutorialImage.Length > 1)
                {
                    GoBackGO.SetActive(true);
                    GoForwardGO.SetActive(true);
                    GoBackBtn.interactable = false;
                    GoForwardBtn.interactable = true;
                }
                else
                {
                    GoBackGO.SetActive(false);
                    GoForwardGO.SetActive(false);
                }
                break;
        }
    }

    public void ClickOnGoToVideo() {
        UICtrl.uICtrl.HideInstructionsCanvas();
        VideoGO.SetActive(true);
        videoPlayer.Play();
    }

    public void ClickOnClose() {
        UICtrl.uICtrl.HideInstructionsCanvas();
    }

    public void VideoHasEnded(UnityEngine.Video.VideoPlayer vp)
    {
        VideoGO.SetActive(false);
        //introCanvasCttrl.VideoHasEnded();
    }

    public void VideoHasEnded()
    {
        videoPlayer.Stop();
        VideoGO.SetActive(false);
        //introCanvasCttrl.VideoHasEnded();
    }


    public void OnclickForwardBtn()
    {
        NavigattionIndex++;
        int limit = 0;
        switch (GeneralCtrl.generalCtrl.SelectedContent) {
            case GeneralCtrl.ContentType.Know:
                IconImage.sprite = KnowTutorialImage[NavigattionIndex];
                InstructionImage.sprite = KnowTutorialText[NavigattionIndex];
                limit = KnowTutorialImage.Length - 1;
                break;
            case GeneralCtrl.ContentType.Scan:
                IconImage.sprite = ScanTutorialImage[NavigattionIndex];
                InstructionImage.sprite = ScanTutorialText[NavigattionIndex];
                limit = ScanTutorialImage.Length -1;
                break;
            case GeneralCtrl.ContentType.Play:
                IconImage.sprite = PlayTutorialImage[NavigattionIndex];
                InstructionImage.sprite = PlayTutorialText[NavigattionIndex];
                limit = PlayTutorialImage.Length -1;
                break;
        }

        if (NavigattionIndex == limit)
        {
            GoForwardBtn.interactable = false;
        }
        if (NavigattionIndex > 0) {
            GoBackBtn.interactable = true;
        }
    }

    public void OnclickBackBtn()
    {
        NavigattionIndex --;
        int limit = 0;
        switch (GeneralCtrl.generalCtrl.SelectedContent)
        {
            case GeneralCtrl.ContentType.Know:
                IconImage.sprite = KnowTutorialImage[NavigattionIndex];
                InstructionImage.sprite = KnowTutorialText[NavigattionIndex];
                limit = KnowTutorialImage.Length - 1;
                break;
            case GeneralCtrl.ContentType.Scan:
                IconImage.sprite = ScanTutorialImage[NavigattionIndex];
                InstructionImage.sprite = ScanTutorialText[NavigattionIndex];
                limit = ScanTutorialImage.Length - 1;
                break;
            case GeneralCtrl.ContentType.Play:
                IconImage.sprite = PlayTutorialImage[NavigattionIndex];
                InstructionImage.sprite = PlayTutorialText[NavigattionIndex];
                limit = PlayTutorialImage.Length - 1;
                break;
        }

        if (NavigattionIndex >= 0 && NavigattionIndex < limit)
        {
            GoForwardBtn.interactable = true;
        }
        if (NavigattionIndex == 0)
        {
            GoBackBtn.interactable = false;
        }
    }

    #region Know
    public void GetKnowTutorial()
    {
        KnowTutorialImage = new Sprite[KnowTutorialImageLink.Length];
        KnowTutorialText = new Sprite[KnowTutorialTextLink.Length];
        KnowImageCount = 0;
        KnowTextCount = 0;
        for (int i = 0; i < KnowTutorialImageLink.Length; i++)
        {
            //Debug.Log("Index on for " + i);
            ContentLoader.contentLoader.StartTextureDownload(KnowTutorialImageLink[i], i, SaveKnowTutorialImage);
            ContentLoader.contentLoader.StartTextureDownload(KnowTutorialTextLink[i], i, SaveKnowTutorialText);
        }

    }

    public void SaveKnowTutorialImage(SpriteDownloaded spriteDownloaded)
    {
        ///Debug.Log("Index " + spriteDownloaded.index);
        KnowTutorialImage[spriteDownloaded.index] = Sprite.Create(spriteDownloaded.texture2D, new Rect(0.0f, 0.0f, spriteDownloaded.texture2D.width, spriteDownloaded.texture2D.height), new Vector2(0.5f, 0.5f));
        CheckHasFinishedKnowTutorialImage();
    }

    public void SaveKnowTutorialText(SpriteDownloaded spriteDownloaded)
    {
        KnowTutorialText[spriteDownloaded.index] = Sprite.Create(spriteDownloaded.texture2D, new Rect(0.0f, 0.0f, spriteDownloaded.texture2D.width, spriteDownloaded.texture2D.height), new Vector2(0.5f, 0.5f));
        CheckHasFinishedKnowTutorialText();
    }

    void CheckHasFinishedKnowTutorialImage()
    {
        KnowImageCount++;
        CheckHasfinished();
    }

    void CheckHasFinishedKnowTutorialText()
    {
        KnowTextCount++;
        CheckHasfinished();
    }
    #endregion

    #region Scan
    public void GetScanTutorial()
    {
        ScanTutorialImage = new Sprite[ScanTutorialImageLink.Length];
        ScanTutorialText = new Sprite[ScanTutorialTextLink.Length];
        ScanImageCount = 0;
        ScanTextCount = 0;
        for (int i = 0; i < ScanTutorialImageLink.Length; i++)
        {
            //Debug.Log("Index on for " + i);
            ContentLoader.contentLoader.StartTextureDownload(ScanTutorialImageLink[i], i, SaveScanTutorialImage);
            ContentLoader.contentLoader.StartTextureDownload(ScanTutorialTextLink[i], i, SaveScanTutorialText);
        }

    }

    public void SaveScanTutorialImage(SpriteDownloaded spriteDownloaded)
    {
        ///Debug.Log("Index " + spriteDownloaded.index);
        ScanTutorialImage[spriteDownloaded.index] = Sprite.Create(spriteDownloaded.texture2D, new Rect(0.0f, 0.0f, spriteDownloaded.texture2D.width, spriteDownloaded.texture2D.height), new Vector2(0.5f, 0.5f));
        CheckHasFinishedScanTutorialImage();
    }

    public void SaveScanTutorialText(SpriteDownloaded spriteDownloaded)
    {
        ScanTutorialText[spriteDownloaded.index] = Sprite.Create(spriteDownloaded.texture2D, new Rect(0.0f, 0.0f, spriteDownloaded.texture2D.width, spriteDownloaded.texture2D.height), new Vector2(0.5f, 0.5f));
        CheckHasFinishedScanTutorialText();
    }

    void CheckHasFinishedScanTutorialImage()
    {
        ScanImageCount++;
        CheckHasfinished();
    }

    void CheckHasFinishedScanTutorialText()
    {
        ScanTextCount++;
        CheckHasfinished();
    }
    #endregion

    #region Play Images
    public void GetPlayTutorial()
    {
        PlayTutorialImage = new Sprite[PlayTutorialImageLink.Length];
        PlayTutorialText = new Sprite[PlayTutorialTextLink.Length];
        PlayImageCount = 0;
        PlayTextCount = 0;
        for (int i = 0; i < PlayTutorialImageLink.Length; i++)
        {
            //Debug.Log("Index on for " + i);
            ContentLoader.contentLoader.StartTextureDownload(PlayTutorialImageLink[i], i, SavePlayTutorialImage);
            ContentLoader.contentLoader.StartTextureDownload(PlayTutorialTextLink[i], i, SavePlayTutorialText);
        }

    }

    public void SavePlayTutorialImage(SpriteDownloaded spriteDownloaded)
    {
        ///Debug.Log("Index " + spriteDownloaded.index);
        PlayTutorialImage[spriteDownloaded.index] = Sprite.Create(spriteDownloaded.texture2D, new Rect(0.0f, 0.0f, spriteDownloaded.texture2D.width, spriteDownloaded.texture2D.height), new Vector2(0.5f, 0.5f));
        CheckHasFinishedPlayTutorialImage();
    }

    public void SavePlayTutorialText(SpriteDownloaded spriteDownloaded)
    {
        PlayTutorialText[spriteDownloaded.index] = Sprite.Create(spriteDownloaded.texture2D, new Rect(0.0f, 0.0f, spriteDownloaded.texture2D.width, spriteDownloaded.texture2D.height), new Vector2(0.5f, 0.5f));
        CheckHasFinishedPlayTutorialText();
    }

    void CheckHasFinishedPlayTutorialImage()
    {
        PlayImageCount++;
        CheckHasfinished();
    }

    void CheckHasFinishedPlayTutorialText()
    {
        PlayTextCount++;
        CheckHasfinished();
    }
    #endregion



    void CheckHasfinished() {
        var hasFinishedPlay = PlayImageCount == PlayTutorialImageLink.Length && PlayTextCount == PlayTutorialTextLink.Length;
        var hasFinisehdScan = ScanImageCount == ScanTutorialImageLink.Length && ScanTextCount == ScanTutorialTextLink.Length;
        var hasFinisehdKnow = KnowImageCount == KnowTutorialImageLink.Length && KnowTextCount == KnowTutorialTextLink.Length;
        if (hasFinisehdKnow && hasFinisehdScan && hasFinishedPlay)
        {
            //UICtrl.uICtrl.HideLoadingDataCanvas();
            selectionCanvasAR.ClickOnScanBtn();


        }
    }
}
