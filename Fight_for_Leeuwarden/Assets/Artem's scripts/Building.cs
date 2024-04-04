using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRender; // The main renderer for the building
    public Vector3Int Size = Vector3Int.one; // The size of the building part in grid units
    public Transform topSocket; // The top socket for stacking the next building part
    public BuildingPartType partType; // Add this field to your Building class

    // This function sets the building to transparent or not, indicating whether it can be placed
    public void SetTransparent(bool available)
    {
        Color color = available ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f); // Semi-transparent
        MainRender.material.color = color;
    }

    // This function reverts the transparency, indicating the building has been placed
    public void SetNormal()
    {
        MainRender.material.color = new Color(1, 1, 1, 1); // Opaque
    }

    // The Unity editor calls this function to draw gizmos if the object is selected
    private void OnDrawGizmosSelected()
    {
        // Draw a semi-transparent cube at the position of the GameObject
        Gizmos.color = new Color(1, 1, 0, 0.5f); // Semi-transparent yellow
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.up * (Size.y * 0.5f), new Vector3(Size.x, Size.y, Size.z)); // Draw at adjusted height based on size
    }
}



