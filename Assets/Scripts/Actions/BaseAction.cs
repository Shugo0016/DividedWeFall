using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public virtual bool IsValidActionAtGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositions = GetValidActionGridList();
        return validGridPositions.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridList();

    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
    }

    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<EnemyAIAction> enemyAIActionList = new List<EnemyAIAction>();

        List<GridPosition> validActionGridPositionList = GetValidActionGridList();

        foreach (GridPosition gridPosition in validActionGridPositionList)
        {
            EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition);
            enemyAIActionList.Add(enemyAIAction);
        }

        if (enemyAIActionList.Count > 0)
        {
            enemyAIActionList.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
            if (enemyAIActionList[0].actionValue == 0)
            {
                List<Unit> units = UnitManager.Instance.GetFriendlyUnitList();
                int distance = 10000;
                Unit nearestUnit = null;
                List<GridPosition> finalPath = null;
                foreach (Unit unitFriend in units)
                {
                    List<GridPosition> path = Pathfinding.Instance.FindPath(unit.GetGridPosition(), unitFriend.GetGridPosition(), out int pathLength);
                    if (path.Count < distance)
                    {
                        distance = path.Count;
                        nearestUnit = unit;
                        finalPath = path;
                    }
                }
                GridPosition finalPosition = new GridPosition(0, 0);
                int counter = 0;
                int count_break = 5;
                if (finalPath.Count < 10)
                {
                    count_break = finalPath.Count - 3;
                }
                foreach (GridPosition position in finalPath)
                {
                    if (counter > count_break)
                    {
                        break;
                    }
                    finalPosition = position;
                    counter += 1;
                }
                return new EnemyAIAction
                {
                    gridPosition = finalPosition,
                    actionValue = 5,
                };
            }
            return enemyAIActionList[0];
        }
        else
        {
            return null;
        }
    }

    public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);
}
