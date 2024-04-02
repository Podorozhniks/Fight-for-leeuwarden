using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public Vector3Int GridSize = new Vector3Int(10, 10, 10); // Now including a Y dimension

    public Building[,,] grid; // A 3D grid
    private Building flyingBuilding;
    private Camera mainCamera;

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
        MiningTransition miningTransition = GetComponent<MiningTransition>();
        miningTransition.IsBuildingPlaced = true;
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 worldPosition = hit.point;
                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);
                float y = Terrain.activeTerrain.SampleHeight(worldPosition);

                bool available = true;
                if (x < 0 || x >= GridSize.x - flyingBuilding.Size.x) available = false;
                if (z < 0 || z >= GridSize.z - flyingBuilding.Size.z) available = false;
                // Y dimension check can be more complex, depending on how you manage vertical stacking or terrain adaptation

                flyingBuilding.transform.position = new Vector3(x, y, z);
                flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding(x, Mathf.RoundToInt(y), z);
                }
            }
        }
    }

    private void PlaceBuilding(int x, int y, int z)
    {
        if (flyingBuilding != null /* && check for position validity */)
        {
            flyingBuilding.SetNormal();
            grid[x, y, z] = flyingBuilding;
            flyingBuilding = null;
        }
        

    }
}