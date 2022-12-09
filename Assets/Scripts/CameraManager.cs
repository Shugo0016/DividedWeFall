using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    private GameObject actionCameraGameObject;

    private void Awake()
    {
        actionCameraGameObject = GameObject.FindWithTag("ActionCam");
        actionCameraGameObject.SetActive(false);
    }

    private void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;
        UnitManager.Instance.OnLevelChange += UnitManager_OnLevelChange;
        actionCameraGameObject.SetActive(false);
    }

    private void UnitManager_OnLevelChange(object sender, EventArgs e)
    {
        actionCameraGameObject.SetActive(false);
    }
    private void ShowActionCamera()
    {
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCameraGameObject.SetActive(false);
    }

    private void BaseAction_OnAnyActionStarted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                Unit shooterUnit = shootAction.GetUnit();
                if (shootAction.GetUnit().GetIsEnemy())
                {
                    return;
                }
                Unit targetUnit = shootAction.GetTargetUnit();
                Vector3 cameraCharHeight = Vector3.up * 3.5f;

                Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;

                float shoulderOffsetAmount = 1.4f;
                Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDir * shoulderOffsetAmount;

                Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + cameraCharHeight + shoulderOffset + (shootDir * -2);

                actionCameraGameObject.transform.position = actionCameraPosition;
                actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharHeight);
                ShowActionCamera();
                break;
        }
    }

    private void BaseAction_OnAnyActionCompleted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }
}
