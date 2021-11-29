using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityStandardAssets.Characters.FirstPerson.FirstPersonController;


public class NewBehaviourScript : MonoBehaviour
{
    Renderer m_Renderer;
    Transform m_Transform;
    bool isInView = false;
    int timesViewed = 0;
    DateTime viewTimeStart = new DateTime(0);
    // UnityEngine.Camera cam1 = FirstPersonController();
    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Transform = GetComponent<Transform>();
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
        //Double viewTimeDelta;
        //Vector3 viewPos = Camera.main.WorldToViewportPoint(m_Transform.position);
        //if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        //{   
        //    if(!isInView){
        //        isInView = true;
        //        timesViewed += 1;
        //        viewTimeStart = DateTime.Now;
        //        Debug.Log("visible");
        //    }
        //}
        //else{
        //    if(isInView){    
        //        viewTimeDelta = DateTime.Now.Subtract(viewTimeStart).TotalSeconds;
        //        if(viewTimeDelta > 0){
        //            Debug.Log("Time Delta = " + viewTimeDelta.ToString());
        //        }
        //        viewTimeStart = new DateTime(0);
        //        isInView = false;
        //        Debug.Log("not visible | times viewed = " + timesViewed.ToString());
        //    }
        //}


    }
}
