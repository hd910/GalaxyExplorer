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
        public GameObject panelBox;
        public GameObject detailsCanvas;

        private int numberOfRows = 3;
        private const string ChronozoomURI = "http://www.chronozoom.com/api/gettimelines?supercollection=chronozoom";

        void Awake()
        {
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
            int currentRow = 0;
            float xOffSet = 0.0533f;
            float yOffset = -0.024f;
            float xPosition = 0f;
            float yPosition = 0f;

            var panelBoxGroup = new GameObject();
            panelBoxGroup.transform.parent = GameObject.Find("ChronozoomContent").transform;
            panelBoxGroup.transform.localPosition = Vector3.zero;
            panelBoxGroup.transform.rotation = Quaternion.identity;
            foreach (Exhibit exhibit in timeline.exhibits)
            {

                //Instantiate the panel box for displaying information
                GameObject panelBoxGameObject = Instantiate(panelBox);
                panelBoxGameObject.transform.parent = panelBoxGroup.transform;
                Vector3 currentPosition = panelBoxGameObject.transform.position;
                if (currentRow == 0)
                {
                    yPosition = 0f;
                }
                else
                {
                    yPosition = yOffset + yPosition;
                }
                panelBoxGameObject.transform.localPosition = new Vector3(currentPosition.x + xPosition, currentPosition.y + yPosition, currentPosition.z);

                //Finds the heading text inside the box and change the title with chronozoom data
                GameObject headingText = panelBoxGameObject.transform.Find("Canvas/Heading").gameObject;
                headingText.GetComponent<Text>().text = exhibit.title;

                //Finds the content text inside the box and change the content with chronozoom data
                GameObject yearText = panelBoxGameObject.transform.Find("Canvas/Year").gameObject;
                yearText.GetComponent<Text>().text = exhibit.time.ToString();

                //Finds the collection text inside the box and change the content with chronozoom data
                GameObject collectionText = panelBoxGameObject.transform.Find("Canvas/Collection").gameObject;
                collectionText.GetComponent<Text>().text = timeline.Regime;

                //Reset row number if exceeds row limit. Increment otherwise
                if(currentRow+1 == numberOfRows)
                {
                    currentRow = 0;
                    xPosition = xOffSet + xPosition;

                }else
                {
                    currentRow++;
                }

            }




            //Use when magic window is needed
            ////Finds the magic window gameobject to display the image
            //GameObject magicWindow = detailsCanvasGameObject.transform.Find("ChronozoomMagicWindow").gameObject;
            //String imageURL = timeline.exhibits[0].contentItems[0].uri;
            //StartCoroutine(LoadImageOntoMagicWindow(magicWindow, imageURL));
        }

        IEnumerator LoadImageOntoMagicWindow(GameObject magicWindow, String imageURL)
        {
            Texture2D tex;
            tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
            WWW www = new WWW(imageURL);
            yield return www;
            www.LoadImageIntoTexture(tex);
            magicWindow.GetComponent<MeshRenderer>().materials[0].mainTexture = tex;
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
