using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class NewBehaviourScript : MonoBehaviour
{
    Renderer m_Renderer;
    Transform m_Transform;
    public GameObject go;
    FirstPersonController m_CharacterController;
    bool isInView = false;
    Dictionary<string,(DateTime startTime, int timesViewed)> isInViewMap = new Dictionary<string,(DateTime startTime, int timesViewed)>();
    // List<(DateTime startTime, int timesViewed)> mylist { get; set; };
    List<string> isInViewList = new List<string>();
    int timesViewed = 0;
    DateTime viewTimeStart = new DateTime(0);
    // UnityEngine.Camera cam1 = FirstPersonController();
    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Transform = GetComponent<Transform>();
    }

    void checkForAd(Vector3 rayTo) {
        Transform player;
        player = GameObject.Find("FPSController").transform;
        m_CharacterController = player.GetComponent<FirstPersonController>();
        RaycastHit iasRayHit;
        if (Physics.Raycast(m_CharacterController.transform.position, rayTo, out iasRayHit))
        {
            // Raycast hit something
            Debug.DrawRay(m_CharacterController.transform.position, Camera.main.WorldToViewportPoint(transform.position) * 5, Color.red, 2, false);
            //Debug.Log("viewed: " + iasRayHit.collider.gameObject.name + " at distance: " + iasRayHit.distance);
            // Get the object that was hit and cast to see if it is a Ad Object
            IASAdComp adComp = iasRayHit.transform.GetComponent<IASAdComp>();
            if (adComp != null)
            {
                // If ad component is availbale notify it for the view
                Debug.Log("** Ad in view ! **");
            }
        }
    }
 
    // Update is called once per frame
    void Update()
    {
        // if (m_Renderer.isVisible)
        // {
        //     Debug.Log("Object is visible");
        // }
        // else if (!m_Renderer.isVisible)
        // {
        //     Debug.Log("Object is no longer visible");
        // }
        // bool isVisibleForCamera1 = cam1.IsObjectVisible(GetComponent<MeshRenderer>());

        // bool isVisibleForCamera1 = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), GetComponent<MeshRenderer>().bounds);
        // if (isVisibleForCamera1){
        //     Debug.Log("visible !");
        // } else {
        //     Debug.Log("Not visible !");
        // }
        
        // checkedObject = this;
        Double viewTimeDelta;
        string placementName = m_Renderer.name;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(m_Transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {   
            // checkForAd(m_Transform.position);
            if(!isInViewList.Contains(placementName)){
                int tempTimesViewed;
                try{
                    tempTimesViewed = isInViewMap[placementName].timesViewed;
                } catch (KeyNotFoundException e){
                    tempTimesViewed = 0;
                }
                viewTimeStart = DateTime.Now;
                // mylist.Add(viewTimeStart,timesViewed);
                // List<(DateTime startTime, int timesViewed)> temp = new List<(DateTime startTime, int timesViewed)>(){ get; set; }
                isInViewMap[placementName] = (viewTimeStart,tempTimesViewed+=1);
                isInViewList.Add(placementName);
                Debug.Log("visible" + m_Renderer.name);
            }
        }
        else{
            if(isInViewList.Contains(placementName)){
                viewTimeStart = isInViewMap[placementName].startTime;    
                viewTimeDelta = DateTime.Now.Subtract(viewTimeStart).TotalSeconds;
                if(viewTimeDelta > 0){
                    Debug.Log(String.Format("{2} => Time Delta = {0} | Viewed {1}X",viewTimeDelta.ToString(),isInViewMap[placementName].timesViewed.ToString(),placementName));
                }
                viewTimeStart = new DateTime(0);
                isInView = false;
                isInViewList.Remove(placementName);
            }
        }
    }
}
