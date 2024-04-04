using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Array to hold references to all cameras
    private int currentCameraIndex; // To keep track of the current camera

    void Start()
    {
        currentCameraIndex = 0; // Start with the first camera

        // Ensure only the first camera is active to start with
        if (cameras.Length > 0)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(i == currentCameraIndex); // Disable all but the first camera
            }
        }
        UpdateCursorState();
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

            UpdateCursorState(); // Update cursor visibility based on the newly active camera
        }
    }

    void UpdateCursorState()
    {
        // Check if the current camera is the top-down camera by index or name
        // Adjust this condition based on your specific setup
        // Example condition: if(currentCameraIndex == indexOfTopDownCamera)
        if (cameras[currentCameraIndex].name == "TopDownCamera") // Replace with your camera's name
        {
            Cursor.visible = true; // Show the cursor
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        }
        else
        {
            Cursor.visible = false; // Hide the cursor for other cameras
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        }
    }
}


