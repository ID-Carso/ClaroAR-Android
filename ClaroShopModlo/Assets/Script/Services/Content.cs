using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Content 
{
    [SerializeField]
    public string ModelLink;
    [SerializeField]
    public string ContentLink;
    [SerializeField]
    public string ARMarket;
    public string[] TutorialTextLink;
    [SerializeField]
    public string[] TutorialImageLink;
    [SerializeField]
    public string[] TutorialTextName;
    [SerializeField]
    public string[] TutorialImageName;
    [SerializeField]
    public string typeContent;
    [SerializeField]
    public bool isActive;
}


[Serializable]
public class RegistedContents {
    [SerializeField]
    public bool status;
    [SerializeField]
    public Content[] realities;
}

public class SpriteDownloaded{
    public int index;
    public Texture2D texture2D;
}

public class DebugSpriteDownloaded
{
    public string name;
    public Texture2D texture2D; 
}


