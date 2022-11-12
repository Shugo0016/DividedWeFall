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
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, System.EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
