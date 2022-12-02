using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveAction : BaseAction
{

    [SerializeField] private int maxMoveDistance = 5;

    public event EventHandler OnStartMoving;

    public event EventHandler OnStopMoving;


    public List<Vector3> positionList;
    private int currentPositionIndex;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        Vector3 targetPosition = positionList[currentPositionIndex];
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

        float stoppingDistance = .1f;

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positionList.Count)
            {
                OnStopMoving?.Invoke(this, EventArgs.Empty);
                ActionComplete();
            }
        }

    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {

        List<GridPosition> pathGridPositionList = Pathfinding.Instance.FindPath(unit.GetGridPosition(), gridPosition, out int pathLength);

        currentPositionIndex = 0;
        positionList = new List<Vector3>();

        foreach (GridPosition pathGridPosition in pathGridPositionList)
        {
            positionList.Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
        }


        OnStartMoving?.Invoke(this, EventArgs.Empty);

        ActionStart(onActionComplete);

    }

    public override List<GridPosition> GetValidActionGridList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }


                if (unitGridPosition == testGridPosition)
                {
                    // Checks if Grid Position has the same position as unit
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //GridPosition Occupied by another unit
                    continue;
                }

                if ((Math.Abs(unitGridPosition.x - testGridPosition.x) + Math.Abs(unitGridPosition.z - testGridPosition.z)) > maxMoveDistance)
                {
                    continue;
                }


                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition))
                {
                    continue;

                }

                if (!Pathfinding.Instance.HasPath(unitGridPosition, testGridPosition))
                {
                    continue;

                }

                int pathfindingDistanceMul = 10;
                if (Pathfinding.Instance.GetPathLength(unitGridPosition, testGridPosition) > maxMoveDistance * pathfindingDistanceMul)
                {
                    continue;
                }


                //Debug.Log(testGridPosition);
                validGridPositions.Add(testGridPosition);
            }

        }


        return validGridPositions;

    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCount = unit.GetShootAction().GetTargetCountAtPosition(gridPosition);
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCount * 10,
        };
    }

}
