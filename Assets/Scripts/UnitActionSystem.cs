using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event System.EventHandler OnSelectedUnitChanged;

    // This Field is used to identify which unit is currently selected. 
    [SerializeField] private Unit selectedUnit;

    // This layer mask will be used to determine which object is an actual unit
    [SerializeField] private LayerMask unitLayerMask;

    // Awake runs before start
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Updates every frame
    private void Update()
    {



        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection())
            {
                return;
            }
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(TouchWorld.GetPosition());

            if(selectedUnit.GetMoveAction().IsValidActionAtGridPosition(mouseGridPosition))
            {
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }
        }

    }



    private bool TryHandleUnitSelection()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, float.MaxValue, unitLayerMask))
        {
            if (hit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                if (unit.GetIsEnemy())
                {
                    return false;
                }
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    // Sets unit that you want to control to controllable unit
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, System.EventArgs.Empty);
    }

    // Gets the current selected unit
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
