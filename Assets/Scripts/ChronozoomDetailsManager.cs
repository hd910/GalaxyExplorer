using GalaxyExplorer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronozoomDetailsManager : MonoBehaviour {

    public List<ContentItem> contentItems { get; set; }
    private int activeIndex;

	// Use this for initialization
	void Start () {
        activeIndex = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initiate()
    {
        DetailsPanel left = (contentItems.Count > 0) ? new DetailsPanel(contentItems[0].title, contentItems[0].description, contentItems[0].uri): new DetailsPanel("", "", "");
        DetailsPanel right = (contentItems.Count > 1) ? new DetailsPanel(contentItems[1].title, contentItems[1].description, contentItems[1].uri) : new DetailsPanel("", "", "");
        DisplayPanelData(left, right);
    }

    public void Next()
    {

    }

    public void Previous()
    {

    }

    private void DisplayPanelData(DetailsPanel left, DetailsPanel right)
    {
        //Finds the left detail panel and change heading with chronozoom data
        GameObject leftDetailHeadingText = this.transform.Find("DetailData/InfoBackPanelLeft/Canvas/leftDetailHeading").gameObject;
        leftDetailHeadingText.GetComponent<Text>().text = left.HeadingText;

        //Finds the left detail panel and change content with chronozoom data
        GameObject leftDetailText = this.transform.Find("DetailData/InfoBackPanelLeft/Canvas/leftDetailDescription").gameObject;
        leftDetailText.GetComponent<Text>().text = left.ContentText;

        //Finds the right detail panel and change heading with chronozoom data
        GameObject rightDetailHeadingText = this.transform.Find("DetailData/InfoBackPanelRight/Canvas/rightDetailHeading").gameObject;
        rightDetailHeadingText.GetComponent<Text>().text = right.HeadingText;

        //Finds the right detail panel and change content with chronozoom data
        GameObject rightDetailText = this.transform.Find("DetailData/InfoBackPanelRight/Canvas/rightDetailDescription").gameObject;
        rightDetailText.GetComponent<Text>().text = right.ContentText;

        //Load up image onto magic window
        GameObject leftMagicWindow = this.transform.Find("DetailData/InfoBackPanelLeft/ChronozoomMagicWindow").gameObject;
        String imageURLLeft = left.ImageURL;
        StartCoroutine(LoadImageOntoMagicWindow(leftMagicWindow, imageURLLeft));

        GameObject rightMagicWindow = this.transform.Find("DetailData/InfoBackPanelRight/ChronozoomMagicWindow").gameObject;
        String imageURLRight = right.ImageURL;
        StartCoroutine(LoadImageOntoMagicWindow(rightMagicWindow, imageURLRight));
    }

    IEnumerator LoadImageOntoMagicWindow(GameObject magicWindow, String imageURL)
    {
        if (imageURL == null || imageURL.Equals(""))
        {
            yield break;
        }
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        WWW www = new WWW(imageURL);
        yield return www;
        www.LoadImageIntoTexture(tex);
        magicWindow.GetComponent<MeshRenderer>().materials[0].mainTexture = tex;
    }
}

public class DetailsPanel
{
    public string HeadingText { get; set; }
    public string ContentText { get; set; }
    public string ImageURL { get; set; }
    public DetailsPanel(string heading, string content, string image)
    {
        HeadingText = heading;
        ContentText = content;
        ImageURL = image;
    }
}
