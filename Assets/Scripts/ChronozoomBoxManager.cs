using GalaxyExplorer;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;

public class ChronozoomBoxManager : GazeSelectionTarget
{
    public static ChronozoomBoxManager ActiveBox;
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
        present = gameObject.GetComponent<ChronozoomPresentToPlayer>();
        if (ChronozoomPresentToPlayer.ActiveExhibit == present)
        {
            ChronozoomPresentToPlayer.ActiveExhibit = null;
            Debug.Log("Reset exhibit");
        }
        else
        {
            ChronozoomPresentToPlayer.ActiveExhibit = present;
            if (present.Presenting)
                return true;

            StartCoroutine(UpdateActive());
        }

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

        while (ChronozoomPresentToPlayer.ActiveExhibit == present)
        {
            //ElementName.GetComponent<MeshRenderer>().material.color = elementNameColor;
            // Wait for the player to send it back
            yield return null;
        }

        animator.SetBool("Opened", false);

        yield return new WaitForSeconds(0.66f); // TODO get rid of magic number        

        // Return the item to its original position
        present.Return();
    }
}
