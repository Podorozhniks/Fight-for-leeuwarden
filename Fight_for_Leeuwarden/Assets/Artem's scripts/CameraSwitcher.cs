using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject topDownCamera;
    public GameObject player;
    public BuildingGrid buildingGrid;
    public GameObject topDownCanvas; // Добавляем ссылку на Canvas для камеры сверху вниз
    [SerializeField] private Button HighlightedButton;

    private bool isTopDownViewActive = false;
    private InputAction switchCameraAction;

    private void Awake()
    {
        // Initialize the InputAction for switching cameras
        switchCameraAction = new InputAction("SwitchCamera", binding: "<Gamepad>/rightShoulder");
        switchCameraAction.performed += _ => SwitchCameraView();
        switchCameraAction.Enable();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCameraView();
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

        // Включаем или отключаем Canvas для камеры сверху вниз
        if (topDownCanvas != null)
        {
            topDownCanvas.SetActive(isTopDownViewActive);
            if (isTopDownViewActive)
            {
                Debug.Log("Active");
                // StartCoroutine(SelectButton());
                HighlightedButton.Select();
            }

        }

        ToggleCursor(isTopDownViewActive);
    }

    IEnumerator SelectButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Tower_Button"));
    }

    void ToggleCursor(bool enable)
    {
        Cursor.visible = enable;
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        // Clean up by disposing of the InputAction when the object is destroyed
        if (switchCameraAction != null)
        {
            switchCameraAction.Disable();
            switchCameraAction.Dispose();
        }
    }
}






