using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject playerCamera; 
    public GameObject topDownCamera; 
    public GameObject player; 
    public BuildingGrid buildingGrid; 

    private bool isTopDownViewActive = false;
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCameraView();
            ToggleCursor(isTopDownViewActive); 
        }
    }

    void SwitchCameraView()
    {
        isTopDownViewActive = !isTopDownViewActive; 

        playerCamera.SetActive(!isTopDownViewActive);
        topDownCamera.SetActive(isTopDownViewActive);

        
        player.SetActive(!isTopDownViewActive);

        
        if (buildingGrid != null)
        {
            buildingGrid.enabled = isTopDownViewActive;
        }
    }

    void ToggleCursor(bool enable)
    {
        Cursor.visible = enable; 
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked; 
    }
}




