using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NewBehaviourScript : MonoBehaviour
{
    public string JsonLink;
    public string JsonName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetJsonData() {
        ContentLoader.contentLoader.StartgettingJsonToSave(JsonLink, JsonName, ProcessData);
    }

    public void ProcessData()
    {
        var JsonPath = FileFinder.fileFinder.GetFilePath(JsonName);


    }

    
}
