using GalaxyExplorer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronozoomMenuControl : GazeSelectionTarget
{
    public GameObject leftButton;
    public GameObject rightButton;
    public string direction;
    public bool isActive;

    private ChronozoomMenuManager menuManager;
    private Color32 originalColor;

	void Start () {
        menuManager = transform.parent.parent.GetComponent<ChronozoomMenuManager>();
        originalColor = GetComponent<Image>().color;

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

    public override void OnGazeSelect()
    {
        //Changes the colour of the box to give a highlighted hover effect
        if (isActive)
            GetComponent<Image>().color = new Color32(26, 67, 124, 255);
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
        if (direction.Equals("right") && isActive)
        {
            menuManager.Next();
        } else if (direction.Equals("left") && isActive)
        {
            menuManager.Previous();
        }
        
        return true;
    }
}
