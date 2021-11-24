using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityStandardAssets.Characters.FirstPerson.FirstPersonController;


public class NewBehaviourScript : MonoBehaviour
{
    Renderer m_Renderer;
    Transform m_Transform;
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
        Vector3 viewPos = Camera.main.WorldToViewportPoint(m_Transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            // Your object is in the range of the camera, you can apply your behaviour
            Debug.Log("visible");
        }
        else
            Debug.Log("not visible");


    }
}
