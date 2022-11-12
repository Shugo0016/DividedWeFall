using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates object on grid 
public class GridObject
{
    private GridSys gridSystem;
    private GridPosition gridPosition;
    private Unit unit;

   
    public GridObject(GridSys gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    // Checks if unit is on space and addes unit name in addition to x and z coordinate
    public override string ToString()
    {
        return gridPosition.ToString() + "\n" + unit;
    }

    // Sets unit
    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    // Gets unit
    public Unit GetUnit()
    {
        return unit;
    }
}
