using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCtrl : MonoBehaviour
{
    public float Point;
    public bool IsNegativePoint;
    public bool CanBeTouched;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        CanBeTouched = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyBox()
    {
        Destroy(this.gameObject, 4f);
    }
}
