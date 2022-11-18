using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;

        UpdateActionPoints();
        CreateUnitActionButtons();
    }
    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
        }
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
        UpdateActionPoints();
    }

    private void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        actionPointsText.text = "ACTION POINTS: " + selectedUnit.GetActionPoints();
    }
}
