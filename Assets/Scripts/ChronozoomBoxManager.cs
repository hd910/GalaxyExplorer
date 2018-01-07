using GalaxyExplorer;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;

public class ChronozoomBoxManager : GazeSelectionTarget
{
    private ChronozoomPresentToPlayer present;

    public void Start()
    {
        // Turn off our animator until it's needed
        GetComponent<Animator>().enabled = false;
        present = GetComponent<ChronozoomPresentToPlayer>();
    }


    public override void OnGazeSelect()
    {
        gameObject.transform.Find("PanelFront").GetComponent<Renderer>().material.color = new Color32(143, 87, 201,255) ;
    }

    public override void OnGazeDeselect()
    {
        gameObject.transform.Find("PanelFront").GetComponent<Renderer>().material.color = new Color32(120, 36, 206,255);
    }

    public override bool OnTapped()
    {
        Debug.Log("Tapped");
        if (present.Presenting)
            return true;

        StartCoroutine(UpdateActive());
        return true;
    }

    public IEnumerator UpdateActive()
    {
        present.Present();

        while (!present.InPosition)
        {
            // Wait for the item to be in presentation distance before animating
            yield return null;
        }

        // Start the animation
        Animator animator = gameObject.GetComponent<Animator>();
        animator.enabled = true;
        animator.SetBool("Opened", true);
    }
}
