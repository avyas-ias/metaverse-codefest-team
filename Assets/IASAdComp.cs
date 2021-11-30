using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class IASAdComp : MonoBehaviour
{
    private readonly static string IAS_AD_SERVER_BASE_URI = "http://localhost:9080/dt";


    public string context;
    public string ageRating;
    public string[] activities;
    public string activities2;


    // Ad ID - Since metaverse is multiplayer each client will have unique ID
    // This ID should actually change whenever Ad changes. For now it is assumed
    // that Ad would not change for simplicity of the project
    public string adSessionId;


    Renderer m_Renderer;
    Camera m_Camera;

    // Get request to notify ad viewed
    IEnumerator NotifyAdViewedToServer()
    {
        string uri = IAS_AD_SERVER_BASE_URI + "/report/" + adSessionId;
        UnityWebRequest req = UnityWebRequest.Get(uri);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error notifying ad server at " + uri);
        } else
        {
            Debug.Log("Ad server notified");
        }

    }

    IEnumerator GetAdToDisplay()
    {
        //string activitiesAsJson = "";
        //for (int i = 0; i < activities.Length; ++i)
        //{
        //    activitiesAsJson = activitiesAsJson + "\"" + activities[i] + "\",";
        //}
        //// remove trailing comma
        //activitiesAsJson = activitiesAsJson.Substring(0, activitiesAsJson.Length - 1);


        string uri = IAS_AD_SERVER_BASE_URI + "/ad";
        string data = "{" +
            "\"publisherId\": \"1\"," +
            "\"context\": \"" + context + "\"," +
            "\"ageRate\": \"" + ageRating + "\"," +
            "\"activities\": [" + activities2 + "]" +
            "}";
        Debug.Log("data: " + data);


        UnityWebRequest req = UnityWebRequest.Post(uri, data);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching ad from " + uri);
        }
        else
        {
            Debug.Log("Ad server responded with : " + req.downloadHandler.text);
        }
    }

    public void viewedBy(FirstPersonController player)
    {
        Debug.Log("I, " + name + ", was viewed by ");// + player.transform.name);

        // Notify Ad server async
        // StartCoroutine("NotifyAdViewedToServer");
    }


    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Camera = FindObjectOfType<Camera>();

        // StartCoroutine("GetAdToDisplay");
    }

    // private void checkAdVisibility()
    // {
    //     Vector3 screenPos = m_Camera.WorldToScreenPoint(transform.position);
    //     bool onScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;

    //     if (onScreen && m_Renderer.isVisible)
    //     {
    //         StartCoroutine("NotifyAdViewedToServer");
    //     }
    //     else
    //     {
    //         //NotVisible
    //     }
    // }



}
