using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingImageScript : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeWhite(float delay)
    {
        Invoke(nameof(animationWhite), delay);
    }

    void animationWhite()
    {
        animator.Play("FadingWhite");
    }
}
