using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates object on grid 
public class GridObject
{
    private GridSys<GridObject> gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;


    public GridObject(GridSys<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    // Displays unit names on grid space
    public override string ToString()
    {
        string unitName = "";
        foreach (Unit unit in unitList)
        {
            unitName += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitName;

    }

    // adds unit to list
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    // Removes unit from list
    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    // Gets unit list
    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    // returns the unit occupying the grid position
    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            return unitList[0];
        }
        else
        {
            return null;
        }
    }
}
