using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public Vector3Int GridSize;
    public Building[,,] grid;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject topDownCamera;

    private Building flyingBuilding;

    public Building baseBuildingPrefab;
    public Building middleBuildingPrefab;
    public Building topBuildingPrefab;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y, GridSize.z];
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        flyingBuilding = Instantiate(buildingPrefab);

        // Disable the BoxCollider while placing
        BoxCollider collider = flyingBuilding.GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

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
        Camera currentCamera = GetCurrentActiveCamera();
        if (flyingBuilding != null && currentCamera != null)
        {
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 worldPosition = hit.point;
                Vector3Int gridPosition = GetGridPosition(worldPosition);

                bool isValidPosition = IsPositionValid(gridPosition.x, gridPosition.y, gridPosition.z);
                flyingBuilding.SetTransparent(isValidPosition);

                flyingBuilding.transform.position = worldPosition;

                if (Input.GetKeyDown(KeyCode.R))
                {
                    RotateBuilding();
                }

                if (isValidPosition && Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding(gridPosition.x, gridPosition.y, gridPosition.z);
                }
            }
        }
    }

    private Camera GetCurrentActiveCamera()
    {
        if (topDownCamera != null && topDownCamera.activeInHierarchy)
        {
            return topDownCamera.GetComponent<Camera>();
        }
        else if (playerCamera != null && playerCamera.activeInHierarchy)
        {
            return playerCamera.GetComponent<Camera>();
        }
        return null;
    }

    private Vector3Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x);
        int y = 0;
        int z = Mathf.FloorToInt(worldPosition.z);
        return new Vector3Int(x, y, z);
    }

    private bool IsPositionValid(int x, int y, int z)
    {
        return x >= 0 && x < GridSize.x && y >= 0 && y < GridSize.y && z >= 0 && z < GridSize.z;
    }

    private Building GetBuildingAt(int x, int y, int z)
    {
        if (x >= 0 && x < GridSize.x && y >= 0 && y < GridSize.y && z >= 0 && z < GridSize.z)
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

            // Enable the BoxCollider after placing
            BoxCollider collider = flyingBuilding.GetComponent<BoxCollider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            flyingBuilding = null;
        }
    }

    private void RotateBuilding()
    {
        if (flyingBuilding != null)
        {
            flyingBuilding.transform.Rotate(0, 45, 0);
        }
    }
}



