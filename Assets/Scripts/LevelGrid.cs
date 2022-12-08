using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGrid : MonoBehaviour
{

    public static LevelGrid Instance { get; private set; }
    [SerializeField] private Transform gridDebugObjectPrefab;

    public event EventHandler OnAnyMoveGridPosition;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    private GridSys<GridObject> gridSystem;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSys<GridObject>(width, height, cellSize,
            (GridSys<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
        //gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    // Start is called before the first frame update
    private void Start()
    {
        Pathfinding.Instance.Setup(width, height, cellSize);
    }

    // Places unit at specific location on grid;
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    // Gets the unit a the specific grid position
    public List<Unit> GetUnitListGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    // When unit leaves position will clear values.
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedPosition(Unit unit, GridPosition from, GridPosition to)
    {
        RemoveUnitAtGridPosition(from, unit);
        AddUnitAtGridPosition(to, unit);
        OnAnyMoveGridPosition?.Invoke(this, EventArgs.Empty);
    }

    // Looks at the world position of Unit and returns location based on grid system
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }


    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPos(gridPosition);


    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    // returns the unit at a particular grid position
    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

}
