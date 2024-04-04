using UnityEngine;

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
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3Int gridPosition = GetGridPosition(hit.point);
                Building buildingAtPosition = GetBuildingAt(gridPosition.x, gridPosition.y, gridPosition.z);

                if (buildingAtPosition == null)
                {
                    flyingBuilding.transform.position = hit.point; // Position at the hit point on the ground
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
        int x = Mathf.FloorToInt(worldPosition.x);
        int y = 0; // This assumes you're placing buildings on a flat surface at y = 0
        int z = Mathf.FloorToInt(worldPosition.z);
        return new Vector3Int(x, y, z);
    }

    private bool IsPositionValid(int x, int y, int z)
    {
        return x >= 0 && x < GridSize.x && y >= 0 && y < GridSize.y && z >= 0 && z < GridSize.z;
    }

    private Building GetBuildingAt(int x, int y, int z)
    {
        if (IsPositionValid(x, y, z))
        {
            return grid[x, y, z];
        }
        return null;
    }

    private void PlaceBuilding(int x, int y, int z)
    {
        if (IsPositionValid(x, y, z) && grid[x, y, z] == null)
        {
            grid[x, y, z] = flyingBuilding;
            flyingBuilding.SetNormal();
            flyingBuilding = null; // Reset after placement
        }
    }
}

