using GalaxyExplorer;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ChronozoomBoxManager : GazeSelectionTarget
{
    public override void OnGazeSelect()
    {
        Debug.Log("Select");
    }

    public override void OnGazeDeselect()
    {
        Debug.Log("Deselect");
    }

    public override bool OnTapped()
    {
        Debug.Log("Tapped");
        return true;
    }
}
