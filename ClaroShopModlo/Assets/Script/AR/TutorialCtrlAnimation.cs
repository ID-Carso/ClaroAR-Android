using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCtrlAnimation : MonoBehaviour
{
    public string AnimationName = "Animation";
    public Animator FactoryAnimator;
    public Animator LibroBoxanimator;
    public Animator XCajaAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGameplay() {
        FactoryAnimator.SetTrigger("Gameplay");
    }

    public void ActiveLibroAnimation()
    {
        LibroBoxanimator.SetBool(AnimationName, true);
    }

    public void DesactiveLibroAnimation()
    {
        LibroBoxanimator.SetBool(AnimationName, false);
    }

    public void ActiveCajaXAnimation()
    {
        XCajaAnimator.SetBool(AnimationName, true);
    }

    public void DesactiveCajaXAnimation()
    {
        XCajaAnimator.SetBool(AnimationName, false);
    }
}
