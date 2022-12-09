using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoAction : BaseAction
{
    private float totalSpinAmount;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        // float spinAddAmount = 360f * Time.deltaTime;
        // transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        // totalSpinAmount += spinAddAmount;
        // if (totalSpinAmount >= 360f)
        // {
        //     ActionComplete();
        // }
    }
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        return;
    }

    public override string GetActionName()
    {
        return "No Action";
    }

    public override List<GridPosition> GetValidActionGridList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition> {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost()
    {
        return 0;
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
