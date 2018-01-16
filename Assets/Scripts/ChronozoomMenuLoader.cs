using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChronozoomMenuLoader : MonoBehaviour {

    private List<PlayableCollection> playableCollectionList;
    private const string ChronozoomCollectionsURI = "http://chronoplayapi.azurewebsites.net:80/api/Counts?APIKey=";
    private const string Key = "2c917dc4aaa343a0817688db82ef275d";

    // Use this for initialization
    void Start () {
        //Change rotation to face camera
        transform.rotation = Camera.main.transform.rotation;

        playableCollectionList = new List<PlayableCollection>();
        StartCoroutine(GetChronozoomCollections());
    }
	
	IEnumerator GetChronozoomCollections()
    {
        Debug.Log("Getting Chronozoom Collections");
        UnityWebRequest www = UnityWebRequest.Get(ChronozoomCollectionsURI + Key);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Retrieved Chronozoom Collections Data");
            DeserializeData(www.downloadHandler.text);
        }
    }

    void DeserializeData(string data)
    {
        playableCollectionList = JsonConvert.DeserializeObject<List<PlayableCollection>>(data);
        DisplayData();
    }

    void DisplayData()
    {

    }
}

public class PlayableCollection
{
    public int idx { get; set; }
    public string SuperCollection { get; set; }
    public string Collection { get; set; }
    public double? Timeline_Count { get; set; }
    public double? Exhibit_Count { get; set; }
    public bool Publish { get; set; }
    public string CZClone { get; set; }
    public string Language { get; set; }
    public string Comment { get; set; }
    public double? Content_Item_Count { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Title { get; set; }
    public string ImageURL { get; set; }
}