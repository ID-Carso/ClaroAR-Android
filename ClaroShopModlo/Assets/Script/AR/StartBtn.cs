using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    public Animator FactoryAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            bool isvalid = Physics.Raycast(ray, out hit, 200);
            Debug.Log("Touch on: " + isvalid);
            if (isvalid)

            {
                FlowBtn touchedObject = hit.transform.gameObject.GetComponent<FlowBtn>();

                if (touchedObject != null)
                {
                    FactoryAnimator.SetTrigger(touchedObject.AnimationName);
                }
            }
        }
    }



}
