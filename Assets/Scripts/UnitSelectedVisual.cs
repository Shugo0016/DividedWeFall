using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bugging out, but it's supposed to show Quad indicator square below unit showing that unit has been selected. 
public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRend;

    private void Awake()
    {
        meshRend = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, System.EventArgs empty)
    {
        UpdateVisual();
    }

    // When switching units enables quad under new unit and disables previous 
    private void UpdateVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            meshRend.enabled = true;
        }
        else
        {
            meshRend.enabled = false;
        }
    }
}
