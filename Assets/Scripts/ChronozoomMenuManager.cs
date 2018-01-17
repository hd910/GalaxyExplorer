using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronozoomMenuManager : MonoBehaviour {

    private int pageNumber = 1;
    private const int numberOfPanels = 3;
    private List<PlayableCollection> playableCollectionList;

    public void Initiate(List<PlayableCollection> collectionList)
    {
        playableCollectionList = collectionList;

        UpdatePanel();
    }

    public void UpdatePanel()
    {
        //Gets current starting index (index of left collection to display)
        int currentStartingIndex = (pageNumber - 1) * numberOfPanels;
        PlayableCollection leftCollection = (playableCollectionList.Count > currentStartingIndex) ? playableCollectionList[currentStartingIndex] : null;
        PlayableCollection centreCollection = (playableCollectionList.Count > currentStartingIndex + 1) ? playableCollectionList[currentStartingIndex + 1] : null;
        PlayableCollection rightCollection = (playableCollectionList.Count > currentStartingIndex + 2) ? playableCollectionList[currentStartingIndex + 2] : null;

        //Update left collection panel
        GameObject leftCollectionText = transform.Find("ChronozoomMenuLeft/CollectionText").gameObject;
        leftCollectionText.GetComponent<Text>().text = (leftCollection != null)?leftCollection.Title:"";
        GameObject leftCollectionMagicWindow = transform.Find("ChronozoomMenuLeft/ChronozoomMagicWindow").gameObject;
        string leftCollectionURL = (leftCollection != null) ? leftCollection.ImageURL : "";
        StartCoroutine(LoadImageOntoMagicWindow(leftCollectionMagicWindow, leftCollectionURL));

        //Update centre collection panel
        GameObject centreCollectionText = transform.Find("ChronozoomMenuCentre/CollectionText").gameObject;
        centreCollectionText.GetComponent<Text>().text = (centreCollection != null) ? centreCollection.Title : "";
        GameObject centreCollectionMagicWindow = transform.Find("ChronozoomMenuCentre/ChronozoomMagicWindow").gameObject;
        string centreCollectionURL = (centreCollection != null) ? centreCollection.ImageURL : "";
        StartCoroutine(LoadImageOntoMagicWindow(centreCollectionMagicWindow, centreCollectionURL));

        //Update right collection panel
        GameObject rightCollectionText = transform.Find("ChronozoomMenuRight/CollectionText").gameObject;
        rightCollectionText.GetComponent<Text>().text = (rightCollection != null) ? rightCollection.Title : "";
        GameObject rightCollectionMagicWindow = transform.Find("ChronozoomMenuRight/ChronozoomMagicWindow").gameObject;
        string rightCollectionURL = (rightCollection != null) ? rightCollection.ImageURL : "";
        StartCoroutine(LoadImageOntoMagicWindow(rightCollectionMagicWindow, rightCollectionURL));
    }

    public void Next()
    {

    }

    public void Previous()
    {

    }

    IEnumerator LoadImageOntoMagicWindow(GameObject magicWindow, string imageURL)
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
