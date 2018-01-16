﻿using GalaxyExplorer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronozoomMenuManager : GazeSelectionTarget
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
        return true;
    }
    }
