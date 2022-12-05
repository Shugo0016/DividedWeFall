using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAction : BaseAction
{
    [SerializeField] private Transform grenadeProjectilePrefab;
    private int maxThrowDistance = 7;
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
    }
    public override string GetActionName()
    {
        return "Grenade";
    }

    public override int GetActionPointsCost()
    {
        return base.GetActionPointsCost();
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }

    public override List<GridPosition> GetValidActionGridList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxThrowDistance; x <= maxThrowDistance; x++)
        {
            for (int z = -maxThrowDistance; z <= maxThrowDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                // restricts the grid to a rhombus shape
                if ((Math.Abs(unitGridPosition.x - testGridPosition.x) + Math.Abs(unitGridPosition.z - testGridPosition.z)) > maxThrowDistance)
                {
                    continue;
                }

                // Debug.Log(testGridPosition);
                validGridPositions.Add(testGridPosition);
            }

        }


        return validGridPositions;
    }

    public override bool IsValidActionAtGridPosition(GridPosition gridPosition)
    {
        return base.IsValidActionAtGridPosition(gridPosition);
    }


    private void OnGrenadeBehaviourComplete()
    {
        ActionComplete();
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        Transform grenadeProjectileTransform = Instantiate(grenadeProjectilePrefab, unit.GetWorldPosition(), Quaternion.identity);
        GrenadeProjectile grenadeProjectile = grenadeProjectileTransform.GetComponent<GrenadeProjectile>();
        grenadeProjectile.Setup(gridPosition, OnGrenadeBehaviourComplete);
        ActionStart(onActionComplete);
    }
}
