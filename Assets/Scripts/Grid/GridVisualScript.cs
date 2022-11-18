using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualScript : MonoBehaviour
{

    public static GridVisualScript Instance { get; private set; }

    [SerializeField] private Transform gridSystemVisualSinglePrefab;

    private GridVisualScriptSingle[,] gridVisualSingleArray;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gridVisualSingleArray = new GridVisualScriptSingle[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()];

        for(int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridVisualSingleArray[x, z] = gridVisualSingleTransform.GetComponent<GridVisualScriptSingle>();

            }

        }
    }

    private void Update()
    {
        UpdateGridVisual();
    }

    public void HideAllGridPositions()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
         
                gridVisualSingleArray[x, z].Hide();

            }

        }
    }

    public void ShowGridPositionsList(List<GridPosition> gridPositions) 
    {
        foreach(GridPosition gridPosition in gridPositions)
        {
            gridVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    private void UpdateGridVisual()
    {
        HideAllGridPositions();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        ShowGridPositionsList(selectedUnit.GetMoveAction().GetValidActionGridList());
    }
  
}
