using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button[] buttons;
    private int currentIndex = 0;

    private UIControls inputActions; // Ensure this matches the name of the generated InputActions class

    private void Awake()
    {
        inputActions = new UIControls();

        inputActions.UI.Navigate.performed += NavigatePerformed;
        inputActions.UI.Submit.performed += SubmitPerformed;  // Make sure this is inside Awake or another initialization method
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    private void OnDestroy()
    {
        inputActions.UI.Navigate.performed -= NavigatePerformed;
        inputActions.UI.Submit.performed -= SubmitPerformed;
        inputActions.Dispose();
    }

    private void NavigatePerformed(InputAction.CallbackContext context)
    {
        Vector2 navigationInput = context.ReadValue<Vector2>();
        Debug.Log("Navigation direction: " + navigationInput);

        if (navigationInput.y > 0)
        {
            currentIndex = Mathf.Max(0, currentIndex - 1);
        }
        else if (navigationInput.y < 0)
        {
            currentIndex = Mathf.Min(buttons.Length - 1, currentIndex + 1);
        }

        buttons[currentIndex].Select();
        Debug.Log("Current selected button: " + buttons[currentIndex].name);
    }
    private void SubmitPerformed(InputAction.CallbackContext context)
    {
        if (currentIndex >= 0 && currentIndex < buttons.Length)
        {
            buttons[currentIndex].onClick.Invoke();
            Debug.Log("Submit performed, button clicked: " + buttons[currentIndex].name);
        }
    }

}



