using System.Collections;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;


public class PlayerEyeComponent : MonoBehaviour
{
    // Time interval in seconds to perform ray-cast
    private readonly static int IAS_AD_CHECK_N_SEC = 1;

    // Store refernce to self's player controller
    public FirstPersonController player;

    public SlidesHandler[] slideHandlers;


    private void Awake()
    {
        // Get reference from unity
        player = GetComponent<FirstPersonController>();

        StartCoroutine("QueueAdCheck");
    }


    // Queue Ad-Check raycast logic for every N seconds
    IEnumerator QueueAdCheck()
    {
        while(true)
        {
            checkForAd();
            yield return new WaitForSeconds(IAS_AD_CHECK_N_SEC);
        }
    }


    private void FixedUpdate()
    {
        //checkForAd();
        //Debug.Log("Fixed update");
    }


    private void Update()
    {
        // Custom inputs
        if (Input.GetKeyUp(KeyCode.E))
        {
            // Next Slide
            checkForSlideManager(1);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            // Prev Slide
            checkForSlideManager(-1);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            // Reset Slides back to 0
            checkForSlideManager(0);
        }
    }

    // Perform raycast to check for Ad Component
    private void checkForSlideManager(int direction)
    {

        for (int i = 0; i < slideHandlers.Length; ++i)
        {
            if (slideHandlers[i] != null)
            {
                slideHandlers[i].changeSlideTo(direction);
            }
        }
    }


    // Perform raycast to check for Ad Component
    private void checkForAd()
    {
        RaycastHit iasRayHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out iasRayHit))
        {
            // Raycast hit something
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * iasRayHit.distance, Color.red, 2, false);
            //Debug.Log("viewed: " + iasRayHit.collider.gameObject.name + " at distance: " + iasRayHit.distance);

            // Get the object that was hit and cast to see if it is a Ad Object
            IASAdComp adComp = iasRayHit.transform.GetComponent<IASAdComp>();
            if (adComp != null)
            {
                // If ad component is availbale notify it for the view
                adComp.viewedBy(player);
            }

        }
    }

}
