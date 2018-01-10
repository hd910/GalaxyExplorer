using GalaxyExplorer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronozoomDetailControl : GazeSelectionTarget
{
    public GameObject leftButton;
    public GameObject rightButton;
    public string direction;
    public bool isActive;

    private ChronozoomDetailsManager detailsManager;
    private Color32 originalColor;

	void Start () {
        detailsManager = transform.parent.parent.parent.GetComponent<ChronozoomDetailsManager>();
        originalColor = GetComponent<Image>().color;
    }

    public override void OnGazeSelect()
    {
        //Changes the colour of the box to give a highlighted hover effect
        if (isActive)
            GetComponent<Image>().color = new Color32(120, 36, 206, 255);
    }

    public override void OnGazeDeselect()
    {
        //Changes the colour back to original
        if (isActive)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 50);
        }
    }

    public override bool OnTapped()
    {
        if (direction.Equals("right"))
        {
            detailsManager.Next();
        } else if (direction.Equals("left"))
        {
            detailsManager.Previous();
        }
        
        return true;
    }
}
