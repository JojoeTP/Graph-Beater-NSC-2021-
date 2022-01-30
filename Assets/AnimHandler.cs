using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool showing;
    Animator animator;
    private void Start()
    {
        showing = false;
        animator = GetComponent<Animator>();
    }

    public void Switch()
    {
        if (showing)
        {
            animator.Play("hidetask");
            showing = false;
        }
        else
        {
            animator.Play("showtask");
            showing = true;
        }

    }
}
