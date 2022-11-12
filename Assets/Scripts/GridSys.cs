using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSys
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

    public GridSys(int width, int height, float cellSize) 
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                //Debug.DrawLine(GetWorldPos(x, z), GetWorldPos(x,z) + Vector3.right * .2f, Color.white, 1000);
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }

    }
    public Vector3 GetWorldPos(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        return new GridPosition(Mathf.RoundToInt(worldPos.x / cellSize), Mathf.RoundToInt(worldPos.y / cellSize));
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject.Instantiate(debugPrefab, GetWorldPos(x, z), Quaternion.identity);
            }
        }
    }
}
