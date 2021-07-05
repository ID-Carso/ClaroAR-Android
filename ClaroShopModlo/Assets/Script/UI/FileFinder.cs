using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileFinder : MonoBehaviour
{
    public string JsonFileName;
    public static FileFinder fileFinder;
    private void Awake()
    {
        if (fileFinder == null) {
            fileFinder = this;
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

    public string GetFilePath(string name)
    {
        if (Directory.Exists(Application.persistentDataPath + "/AssetsClaroShopAR"))
        {
            Debug.Log("File Exist");
            string worldsFolder = Application.persistentDataPath + "/AssetsClaroShopAR";

            DirectoryInfo d = new DirectoryInfo(worldsFolder);
            foreach (var file in d.GetFiles("*.json" ))
            {
                return file.FullName;
            }
        }
        else
        {
            File.Create(Application.persistentDataPath);
            
        }
        return "";
    }

    public Content[] GetJsonData(string jsonPath)
    {
        var jsonString = File.ReadAllText(jsonPath);
        return JsonUtility.FromJson<RegistedContents>(jsonString).realities;
    }
}
