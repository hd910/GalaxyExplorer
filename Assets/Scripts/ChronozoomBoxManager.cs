using GalaxyExplorer;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ChronozoomBoxManager : GazeSelectionTarget
{
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
        return true;
    }
}
