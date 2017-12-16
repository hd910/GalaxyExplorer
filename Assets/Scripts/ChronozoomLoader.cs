using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace GalaxyExplorer
{
    public class ChronozoomLoader : MonoBehaviour
    {
        public GameObject text;
        public GameObject detailsCanvas;

        private const string ChronozoomURI = "http://www.chronozoom.com/api/gettimelines?supercollection=chronozoom";

        void Awake()
        {
            //TODO: Need to change loader to better position. Right now called from Earth prefab to demonstrate it working
            if (GameObject.Find("ChronozoomDetails(Clone)") == null)
                StartCoroutine(GetChronozoomData());
        }

        IEnumerator GetChronozoomData()
        {
            Debug.Log("Getting Chronozoom Data");
            UnityWebRequest www = UnityWebRequest.Get(ChronozoomURI);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Retrieved Chronozoom Data");
                DeserializeData(www.downloadHandler.text);
            }
        }

        void DeserializeData(string data)
        {
            
            Timeline timeline = new Timeline();
            timeline = JsonConvert.DeserializeObject<Timeline>(data);
            DisplayData(timeline);
        }

        void DisplayData(Timeline timeline)
        {
            //GameObject textGameObject = Instantiate(text);
            //int zPosition = 3;
            //foreach(Exhibit exhibits in timeline.exhibits)
            //{
            //    //Debug.Log(exhibits.title + " " + exhibits.time);
            //    GameObject textGameObject = Instantiate(text);
            //    textGameObject.transform.position = new Vector3(textGameObject.transform.position.x, textGameObject.transform.position.y, zPosition);

            //    textGameObject.GetComponent<TextMesh>().text = exhibits.title + " " + exhibits.time;

            //    zPosition += 1;
            //}
            //Instantiate the main canvas for displaying information
            GameObject detailsCanvasGameObject = Instantiate(detailsCanvas);

            //Finds the heading text inside the canvas and change the title with chronozoom data
            GameObject headingText = detailsCanvasGameObject.transform.Find("BackgroundWhite/HeadingText").gameObject;
            headingText.GetComponent<Text>().text = timeline.exhibits[0].title;

            //Finds the content text inside the canvas and change the content with chronozoom data
            GameObject contentText= detailsCanvasGameObject.transform.Find("BackgroundWhite/ContentText").gameObject;
            contentText.GetComponent<Text>().text = timeline.exhibits[0].contentItems[0].description;


        }

    }

    public class Timeline
    {
        public string __type { get; set; }
        public bool FromIsCirca { get; set; }
        public string Height { get; set; }
        public string Regime { get; set; }
        public bool ToIsCirca { get; set; }
        public float? aspectRatio { get; set; }
        public string backgroundUrl { get; set; }
        public string end { get; set; }
        public Exhibit[] exhibits { get; set; }
        public string id { get; set; }
        public string offsetY { get; set; }
        public string start { get; set; }
        public Timeline[] timelines { get; set; }
        public string title { get; set; }
    }

    public class Exhibit
    {
        public string __type { get; set; }
        public bool IsCirca { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedTime { get; set; }
        public List<ContentItem> contentItems { get; set; }
        public string id { get; set; }
        public string offsetY { get; set; }
        public Int64 time { get; set; }
        public string title { get; set; }
        public string ParentTimeLineId { get; set; }
    }

    public class ContentItem
    {
        public string __type { get; set; }
        public Int64 year { get; set; }
        public string attribution { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string mediaSource { get; set; }
        public string mediaType { get; set; }
        public int order { get; set; }
        public string title { get; set; }
        public string uri { get; set; }
        public string ParentExhibitId { get; set; }
        public Int64 ParentExhibitTime { get; set; }
    }
}
