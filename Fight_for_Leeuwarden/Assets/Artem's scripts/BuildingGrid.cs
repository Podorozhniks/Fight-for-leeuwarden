using UnityEngine;
using UnityEngine.InputSystem;

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

    private InputAction moveAction;
    private InputAction rotateAction;
    private InputAction placeAction;

    private Vector3 currentBuildingPosition;

    [SerializeField] private float moveSpeedX = 10f; // Speed for X-axis movement
    [SerializeField] private float moveSpeedZ = 10f; // Speed for Z-axis movement
    [SerializeField] private LayerMask terrainLayerMask; // Layer mask for the terrain

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y, GridSize.z];

        moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick");
        rotateAction = new InputAction("Rotate", binding: "<Gamepad>/buttonWest"); // Usually 'X' on Xbox or 'Square' on PlayStation
        placeAction = new InputAction("Place", binding: "<Gamepad>/buttonSouth");

        moveAction.Enable();
        rotateAction.Enable();
        placeAction.Enable();

        rotateAction.performed += _ => RotateBuilding();
        placeAction.performed += _ => PlaceCurrentBuilding();

        currentBuildingPosition = Vector3.zero; // Initialize the building position
    }

    private void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        flyingBuilding = Instantiate(buildingPrefab);
        if (flyingBuilding.GetComponent<BoxCollider>() != null)
        {
            flyingBuilding.GetComponent<BoxCollider>().enabled = false;
        }
        currentBuildingPosition = GetInitialSpawnPosition(); // Set initial spawn position on the grid
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

    void Update()
    {
        if (flyingBuilding != null)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            // Обновляем текущую позицию здания с учетом скорости для осей X и Z
            currentBuildingPosition += new Vector3(moveInput.x * moveSpeedX, 0, moveInput.y * moveSpeedZ) * Time.deltaTime;

            // Debug log the current position
            Debug.Log($"Current Building Position: {currentBuildingPosition}");

            Camera currentCamera = GetCurrentActiveCamera();

            // Получаем направление движения от текущей камеры
            Vector3 forward = currentCamera.transform.forward;
            Vector3 right = currentCamera.transform.right;

            forward.y = 0; // Игнорируем компонент Y
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = (right * moveInput.x + forward * moveInput.y) * Time.deltaTime;
            currentBuildingPosition += moveDirection;

            // Перемещаем здание по сетке
            Vector3 worldPosition = new Vector3(
                Mathf.Round(currentBuildingPosition.x / 1) * 1,
                currentBuildingPosition.y,
                Mathf.Round(currentBuildingPosition.z / 1) * 1
            );

            // Бросаем луч вниз от позиции здания, чтобы найти поверхность террейна
            Ray ray = new Ray(worldPosition + Vector3.up * 10, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, terrainLayerMask))
            {
                Vector3Int gridPosition = GetGridPosition(hit.point);

                // Debug log the hit position and grid position
                Debug.Log($"Hit Position: {hit.point}, Grid Position: {gridPosition}");

                worldPosition.y = hit.point.y; // Устанавливаем высоту здания на высоте террейна
                flyingBuilding.transform.position = worldPosition;

                if (IsPositionValid(gridPosition.x, gridPosition.y, gridPosition.z))
                {
                    flyingBuilding.SetTransparent(true);
                }
                else
                {
                    flyingBuilding.SetTransparent(false);
                }
            }
        }
    }

    private Camera GetCurrentActiveCamera()
    {
        return topDownCamera.activeSelf ? topDownCamera.GetComponent<Camera>() : playerCamera.GetComponent<Camera>();
    }

    private Vector3Int GetGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(Mathf.FloorToInt(worldPosition.x), 0, Mathf.FloorToInt(worldPosition.z));
    }

    private bool IsPositionValid(int x, int y, int z)
    {
        bool isValid = x >= 0 && x < GridSize.x && y >= 0 && y < GridSize.y && z >= 0 && z < GridSize.z;

        // Debug log to check if the position is valid
        Debug.Log($"Position Valid: {isValid} for Coordinates: {x}, {y}, {z}");

        return isValid;
    }

    private void PlaceCurrentBuilding()
    {
        if (flyingBuilding != null)
        {
            Vector3 worldPosition = flyingBuilding.transform.position;
            Vector3Int gridPosition = GetGridPosition(worldPosition);
            if (IsPositionValid(gridPosition.x, gridPosition.y, gridPosition.z))
            {
                PlaceBuilding(gridPosition.x, gridPosition.y, gridPosition.z);
            }
        }
    }

    private void PlaceBuilding(int x, int y, int z)
    {
        if (IsPositionValid(x, y, z) && grid[x, y, z] == null)
        {
            grid[x, y, z] = flyingBuilding;
            flyingBuilding.SetNormal();
            if (flyingBuilding.GetComponent<BoxCollider>() != null)
            {
                flyingBuilding.GetComponent<BoxCollider>().enabled = true;
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

    private void OnDestroy()
    {
        moveAction.Dispose();
        rotateAction.Dispose();
        placeAction.Dispose();
    }

    private Vector3 GetInitialSpawnPosition()
    {
        // Начальная позиция на сетке
        Vector3 initialPosition = new Vector3(GridSize.x / 2, 0, GridSize.z / 2);
        Ray ray = new Ray(initialPosition + Vector3.up * 10, Vector3.down); // Начинаем луч немного выше плоскости

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, terrainLayerMask))
        {
            return hit.point;
        }

        return initialPosition;
    }
}































