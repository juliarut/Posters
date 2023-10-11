using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneController : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager planeManager;

    // Funktion f�r att v�xla mellan att visa och d�lja alla plan som har trackats
    public void TogglePlaneVisibility()
    {
        // Kontrollera att planeManager �r inst�lld och inte null
        if (planeManager != null)
        {
            // V�xla planeManager mellan aktiverad och inaktiverad
            planeManager.enabled = !planeManager.enabled;

            // F�r varje plan i planeManager-trackables, aktivera eller inaktivera dess GameObject
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(planeManager.enabled);
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
}