using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
//http://objects-us-east-1.dream.io/arassetsinfinitum/android/mynewbboundle
public class BundleWebLoader : MonoBehaviour
{
    //public string bundleUrl = "http://objects-us-east-1.dream.io/arassetsinfinitum/android/claroshopscan";
    public string bundleUrl = "http://objects-us-east-1.dream.io/arassetsinfinitum/android/factory";
    public string assetName = "BundledObject";

    // Start is called before the first frame update
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("Network error");
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (bundle != null)
            {
                string rootAssetPath = bundle.GetAllAssetNames()[0];
                GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, transform);
                bundle.Unload(false);
                //callback(arObject);
            }
            else
            {
                Debug.Log("Not a valid asset bundle");
            }
        }
    }


}
