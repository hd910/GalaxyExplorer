using GalaxyExplorer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronozoomMagicWindowControl : GazeSelectionTarget
{

    public float TravelTime = 1f;
    public float PresentationDistance = 0.5f;

    private bool isMagnified = false;

    public override bool OnTapped()
    {
        Animator animator = gameObject.GetComponent<Animator>();

        if (!isMagnified)
        {
            // Start the animation
            animator.enabled = true;
            animator.SetBool("Opened", true);
            isMagnified = true;
        }
        else
        {
            // Close the animation
            animator.SetBool("Opened", false);
            animator.enabled = true;
            isMagnified = false;
        }
        

        return true;
    }
}
