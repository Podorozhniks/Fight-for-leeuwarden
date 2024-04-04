using UnityEngine;

public enum BuildingPartType
{
    Base,
    Middle,
    Top
}

public class BuildingGrid : MonoBehaviour
{
    public Vector3Int GridSize; // Set this in the Unity Inspector
    public Building[,,] grid; // A 3D grid to store buildings

    private Building flyingBuilding; // The building currently being moved before placement
    private Camera mainCamera; // Reference to the main camera for raycasting

    // Assign these in the Unity Editor
    public Building baseBuildingPrefab;
    public Building middleBuildingPrefab;
    public Building topBuildingPrefab;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y, GridSize.z];
        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        flyingBuilding = Instantiate(buildingPrefab);
    }

    // Connect these methods to UI buttons for placing buildings
    public void PlaceBase()
    {
        StartPlacingBuilding(baseBuildingPrefab);
    }

    public void PlaceMiddle()
    {
        StartPlacingBuilding(middleBuildingPrefab);
    }

    public void PlaceTop()
    {
        StartPlacingBuilding(topBuildingPrefab);
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // Declare 'hit' here to be in scope for later use.
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3Int gridPosition = GetGridPosition(hit.point);
                Building belowBuilding = GetBuildingAt(gridPosition.x, gridPosition.y - 1, gridPosition.z);

                if (flyingBuilding.partType == BuildingPartType.Base && belowBuilding == null ||
                    flyingBuilding.partType == BuildingPartType.Middle && belowBuilding?.partType == BuildingPartType.Base ||
                    flyingBuilding.partType == BuildingPartType.Top && belowBuilding?.partType == BuildingPartType.Middle)
                {
                    flyingBuilding.transform.position = belowBuilding != null ? belowBuilding.topSocket.position : hit.point;
                    flyingBuilding.SetTransparent(true);

                    if (Input.GetMouseButtonDown(0)) // Left mouse button
                    {
                        PlaceBuilding(gridPosition.x, gridPosition.y, gridPosition.z);
                    }
                }
                else
                {
                    flyingBuilding.SetTransparent(false);
                }
            }
        }
    }

    private Vector3Int GetGridPosition(Vector3 worldPosition)
    {
        // Example implementation for GetGridPosition
        // This would need to be adjusted to your specific game grid logic
        int x = Mathf.FloorToInt(worldPosition.x);
        int y = 0; // Assuming ground level is 0 and you'll adjust this for stacking logic
        int z = Mathf.FloorToInt(worldPosition.z);
        return new Vector3Int(x, y, z);
    }

    private bool IsPositionValid(int x, int y, int z)
    {
        // Example implementation for IsPositionValid
        // Check that the position is within the bounds of the grid
        return x >= 0 && x < GridSize.x &&
               y >= 0 && y < GridSize.y &&
               z >= 0 && z < GridSize.z;
    }

    private Building GetBuildingAt(int x, int y, int z)
    {
        // Check the bounds of the grid first
        if (x >= 0 && x < GridSize.x && y >= 0 && y < GridSize.y && z >= 0 && z < GridSize.z)
        {
            return grid[x, y, z];
        }
        return null;
    }

    private void PlaceBuilding(int x, int y, int z)
    {
        if (IsPositionValid(x, y, z))
        {
            grid[x, y, z] = flyingBuilding;
            flyingBuilding.SetNormal();
            flyingBuilding = null; // Reset after placement
        }
    }
}
