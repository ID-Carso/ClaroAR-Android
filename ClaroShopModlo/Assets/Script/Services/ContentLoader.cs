using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Text;
using System.IO;

public class ContentLoader : MonoBehaviour
{
    public static ContentLoader contentLoader;

    private void Awake()
    {
        if (contentLoader == null) {
            contentLoader = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGODownload(string contentURL, UnityAction<GameObject> callBack) {
        StartCoroutine(GetARGOContent(contentURL, callBack));
    }

    public void StartTextureDownload(string textureURL, UnityAction<Texture2D> callBack) {
        StartCoroutine(GetTexture(textureURL, callBack));
    }

    public void StartTextureDownload(string textureURL, int index, UnityAction<SpriteDownloaded> callBack)
    {
        StartCoroutine(GetTexture(textureURL, index, callBack));
    }

    public void StartTextureDownload(string textureURL, string name, UnityAction<DebugSpriteDownloaded> callBack)
    {
        StartCoroutine(GetTexture(textureURL, name, callBack));
    }

    public void StartGettingJson(string jsonURL, UnityAction<Content[]> callBack) {
        StartCoroutine(GetJsonData(jsonURL, callBack));
    }

    public void StartgettingJsonToSave(string jsonURL, string name, UnityAction callBack)
    {
        StartCoroutine(GetJsonDataToSave(jsonURL, name, callBack));
    }

    public IEnumerator GetARGOContent(string contentURL, UnityAction<GameObject> callBack) {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(contentURL);
        yield return www.SendWebRequest();
        
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Network error");
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (bundle != null)
            {
                //Debug.Log("DEBUG: Game Object downloaded from: " + contentURL);
                string rootAssetPath = bundle.GetAllAssetNames()[0];
                callBack(bundle.LoadAsset(rootAssetPath) as GameObject);
                bundle.Unload(false);
                //callback(arObject);
            }
            else
            {
                Debug.Log("Not a valid asset bundle");
            }
        }
    }

    public IEnumerator GetTexture(string textureURL, UnityAction<Texture2D> callBack)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("DEBUG: Texture downloaded from: " + textureURL);
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            callBack(myTexture);
        }
    }

    public IEnumerator GetTexture(string textureURL, int index, UnityAction<SpriteDownloaded> callBack)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("DEBUG: Texture downloaded from: " + textureURL);
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            var result = new SpriteDownloaded();
            result.index = index;
            result.texture2D = myTexture;
            callBack(result);
        }
    }

    public IEnumerator GetTexture(string textureURL, string name, UnityAction<DebugSpriteDownloaded> callBack)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("DEBUG: Texture downloaded from: " + textureURL);
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            var result = new DebugSpriteDownloaded();
            result.name = name;
            result.texture2D = myTexture;
            callBack(result);
        }
    }


    public IEnumerator GetJsonData(string jsonURL, UnityAction<Content[]> callBack) {
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            string jsonString =  Encoding.ASCII.GetString(results);
            //Debug.Log("Result: " + jsonString);
            Content[] resultContents = JsonUtility.FromJson<RegistedContents>(jsonString).realities;
            callBack(resultContents);
        }
    }

    IEnumerator GetJsonDataToSave(string jsonURL, string name, UnityAction callBack)
    {
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
//Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            string jsonString = Encoding.ASCII.GetString(results);
            //File.Delete(Application.persistentDataPath + "/AssetsClaroShopAR");
            if (!Directory.Exists(Application.persistentDataPath + "/AssetsClaroShopAR")) {
                Directory.CreateDirectory(Application.persistentDataPath + "/AssetsClaroShopAR");
            }
            string savePath = string.Format("{0}/AssetsClaroShopAR/{1}", Application.persistentDataPath, name);
            Debug.Log("Result: " + savePath);
            System.IO.File.WriteAllText(savePath, jsonString);
            callBack();
        }

        
    }
}