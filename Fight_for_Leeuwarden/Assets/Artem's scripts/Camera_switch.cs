using UnityEngine;

public class Camera_switch : MonoBehaviour
{
    public Camera[] cameras; // Array to hold references to all cameras
    private int currentCameraIndex; // To keep track of the current camera

    void Start()
    {
        currentCameraIndex = 0; // Start with the first camera

        // Ensure only the first camera is active to start with
        if (cameras.Length > 0)
        {
            for (int i = 1; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(false); // Disable all cameras except the first one
            }
        }
        // Optionally, log an error if no cameras are assigned
        else
        {
            Debug.LogError("No cameras set in the CameraSwitcher script.");
        }
    }

    void Update()
    {
        // When the Tab key is pressed, switch cameras
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Disable the currently active camera
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Increment the index to switch to the next camera
            // If we're at the last camera, loop back to the first
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Enable the next camera
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}

