using GalaxyExplorer;
using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronozoomMenuItemManager : GazeSelectionTarget
{
    public override void OnGazeSelect()
    {
        //Changes the colour of the box to give a highlighted hover effect
        gameObject.transform.Find("BackgroundPanel").GetComponent<Renderer>().material.color = new Color32(80, 133, 159, 255);
    }

    public override void OnGazeDeselect()
    {
        //Changes the colour back to original
        gameObject.transform.Find("BackgroundPanel").GetComponent<Renderer>().material.color = new Color32(26, 67, 124, 255);
    }

    public override bool OnTapped()
    {
        TransitionManager.Instance.LoadNextScene("ChronozoomView", gameObject);
        return true;
    }
}
