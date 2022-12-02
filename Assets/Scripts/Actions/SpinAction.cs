using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360f)
        {
            ActionComplete();
        }
    }
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        totalSpinAmount = 0f;
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridList()
    {
        // List<GridPosition> validGridPositions = new List<GridPosition>();
        // GridPosition unitGridPosition = unit.GetGridPosition();

        // for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        // {
        //     for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
        //     {
        //         GridPosition offsetGridPosition = new GridPosition(x, z);
        //         GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
        //         if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
        //         {
        //             continue;
        //         }


        //         if (unitGridPosition == testGridPosition)
        //         {
        //             // Checks if Grid Position has the same position as unit
        //             continue;
        //         }

        //         if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
        //         {
        //             //GridPosition Occupied by another unit
        //             continue;
        //         }

        //         if ((Math.Abs(unitGridPosition.x - testGridPosition.x) + Math.Abs(unitGridPosition.z - testGridPosition.z)) > maxMoveDistance)
        //         {
        //             continue;
        //         }


        //         //Debug.Log(testGridPosition);
        //         validGridPositions.Add(testGridPosition);
        //     }

        // }


        // return validGridPositions;
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition> {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }


    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }
}
