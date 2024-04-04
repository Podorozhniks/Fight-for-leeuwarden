using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer; // This should be your building's renderer
    // Other properties and methods as needed for the Building class

    // Methods like SetTransparent and SetNormal should be defined here
    public void SetTransparent(bool available)
    {
        MainRenderer.material.color = available ? new Color(1, 1, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
    }

    public void SetNormal()
    {
        MainRenderer.material.color = new Color(1, 1, 1, 1);
    }
}




