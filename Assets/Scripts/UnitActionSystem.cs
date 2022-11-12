using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event System.EventHandler OnSelectedUnitChanged;
   [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

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
    private void Update()
    {
        
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            HandleUnitSelect();
            selectedUnit.Move(TouchWorld.GetPosition());
        }
    }

    // Check if ray hits a different controlable unit and allows user to move that specific unit.
    private void HandleUnitSelect()
    {
        Ray ray;
        RaycastHit hit;
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, float.MaxValue, unitLayerMask))
            {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    selectedUnit = unit;

                }
            }
        }
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
