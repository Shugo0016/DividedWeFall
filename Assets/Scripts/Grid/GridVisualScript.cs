using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridVisualScript : MonoBehaviour
{

    public static GridVisualScript Instance { get; private set; }

    [Serializable]
    public struct GridVisTypeMaterial
    {
        public GridVisType gridVisType;
        public Material material;
    }
    public enum GridVisType
    {
        White,
        Blue,
        Red,
        Yellow,
        RedSoft,
    }

    [SerializeField] private List<GridVisTypeMaterial> gridVisTypeMaterialList;
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

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridVisualSingleArray[x, z] = gridVisualSingleTransform.GetComponent<GridVisualScriptSingle>();

            }

        }

        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        LevelGrid.Instance.OnAnyMoveGridPosition += LevelGrid_OnAnyMoveGridPosition;
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

    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisType gridVisType)
    {
        List<GridPosition> gridPosList = new List<GridPosition>();
        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {

                GridPosition testGridPos = gridPosition + new GridPosition(x, z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPos))
                {
                    continue;
                }


                // restricts the grid to a rhombus shape
                if ((Math.Abs(x) + Math.Abs(z)) > range)
                {
                    continue;
                }



                gridPosList.Add(testGridPos);

            }
        }

        ShowGridPositionsList(gridPosList, gridVisType);
    }

    public void ShowGridPositionsList(List<GridPosition> gridPositions, GridVisType gridVisType)
    {
        foreach (GridPosition gridPosition in gridPositions)
        {
            Debug.Log(gridPosition);
            gridVisualSingleArray[gridPosition.x, gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisType));
        }
    }

    private void UpdateGridVisual()
    {
        // Debug.Log("This ran");
        HideAllGridPositions();
        // Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        BaseAction actionSelected = UnitActionSystem.Instance.GetSelectedAction();

        GridVisType gridVisType;

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        switch (actionSelected)
        {
            default:
            case MoveAction moveAction:
                gridVisType = GridVisType.White;
                break;
            case SpinAction spinAction:
                gridVisType = GridVisType.Blue;
                break;
            case ShootAction shootAction:
                gridVisType = GridVisType.Red;

                ShowGridPositionRange(selectedUnit.GetGridPosition(), shootAction.GetMaxShootDistance(), GridVisType.RedSoft);
                break;
        }

        ShowGridPositionsList(actionSelected.GetValidActionGridList(), gridVisType);
    }


    private Material GetGridVisualTypeMaterial(GridVisType gridVisType)
    {
        foreach (GridVisTypeMaterial gridVisTypeMaterial in gridVisTypeMaterialList)
        {
            if (gridVisTypeMaterial.gridVisType == gridVisType)
            {
                return gridVisTypeMaterial.material;
            }
        }
        Debug.LogError("Could not find GridVisualTypeMaterial for GridVisualType" + gridVisType);
        return null;
    }

    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    private void LevelGrid_OnAnyMoveGridPosition(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }
}
