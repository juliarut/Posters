using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceShowHidePlane : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToInstantiate;

    private GameObject spawnedObject = null;

    [SerializeField]
    private ARRaycastManager raycastManager;

    private static List<ARRaycastHit> hitList = new List<ARRaycastHit>();

    [SerializeField]
    private ARPlaneManager planeManager;

    bool TryGetTouchPosition(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if (raycastManager.Raycast(touchPosition, hitList, TrackableType.Planes))
        {
            Pose hitPose = hitList[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(prefabToInstantiate, hitPose.position, hitPose.rotation);

                TogglePlaneDetection();
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
        }


    }

    // Funktion f�r att v�xla mellan att visa och d�lja alla plan som har trackats
    public void TogglePlaneVisibilityStillTracking()
    {
        // Kontrollera att planeManager �r inst�lld och inte null
        if (planeManager != null)
        {
            // V�xla planeManager mellan aktiverad och inaktiverad
            // Tack vare vi g�r denna rad kommer vi �ven "f�rst�ra" tidigare trackingen f�r dessa plan
            planeManager.enabled = !planeManager.enabled;

            // F�r varje plan i planeManager-trackables, aktivera eller inaktivera dess GameObject
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(planeManager.enabled);
            }
        }
    }

    /// <summary>
    /// Detta d�ljer/visar UTAN att f�rst�ra dem
    /// </summary>
    public void TogglePlaneVisibility()
    {
        if (planeManager != null)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(!plane.gameObject.activeSelf);
            }
        }
    }


    // Funktion f�r att sluta tracka plan
    public void StopPlaneTracking()
    {
        // Kontrollera att planeManager �r inst�lld och inte null
        if (planeManager != null)
        {
            // Inaktivera planeManager f�r att sluta tracka plan
            planeManager.enabled = false;
        }
    }

    // Funktion f�r att �teruppta tracking av plan
    public void ResumePlaneTracking()
    {
        // Kontrollera att planeManager �r inst�lld och inte null
        if (planeManager != null)
        {
            // Aktivera planeManager f�r att �teruppta tracking av plan
            planeManager.enabled = true;
        }
    }





    bool isPlaneToggled;

    public void TogglePlaneDetection()
    {
        /*
        if (isPlaneToggled)
        {
            isPlaneToggled = false;
        } else
        {
            isPlaneToggled = true;
        }
        */

        isPlaneToggled = !isPlaneToggled;

        planeManager.SetTrackablesActive(isPlaneToggled);
        // planeManager.planePrefab.SetActive(isPlaneToggled);

        foreach (ARPlane plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(isPlaneToggled);
        }
    }
}
