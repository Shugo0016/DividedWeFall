using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{


    public static LevelSetup Instance { get; private set; }
    [SerializeField] private Transform gridDebugPrefab;
    private GridSys gridSystem;

    // Sets dimensions of grid I.E cellSize, height, width
    private void Awake()
    {
        // Checks if there is more then one level grid objects if there is it will destroy the extra
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        gridSystem = new GridSys(15, 15, 5f);
        gridSystem.CreateDebugObjects(gridDebugPrefab);
    }

    // Supposed to set unit at a grid position. 
    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.SetUnit(unit);
    }

    // Gets grid position of unit
    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

    // After unit leaves space sets that previous space to null.
    public void ClearUnitAtGridPosition(GridPosition gridPosition)
    {

        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.SetUnit(null);
    }

    public GridPosition GetGridPosition(Vector3 worldPos) => gridSystem.GetGridPosition(worldPos);
}
