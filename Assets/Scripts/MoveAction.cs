using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveAction : MonoBehaviour
{

    [SerializeField] private int maxMoveDistance = 5;

    public Vector3 targetPosition;
    private Unit unit;

    private void Awake()
    {

        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
    }

    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition); 
        
    }

    public bool IsValidActionAtGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositions = GetValidActionGridList();
        return validGridPositions.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }


                if(unitGridPosition == testGridPosition)
                {
                    // Checks if Grid Position has the same position as unit
                    continue;
                }

                if(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //GridPosition Occupied by another unit
                    continue;
                }

                if ((Math.Abs(unitGridPosition.x - testGridPosition.x) + Math.Abs(unitGridPosition.z - testGridPosition.z)) > maxMoveDistance) 
                {
                    continue;
                }


                //Debug.Log(testGridPosition);
                validGridPositions.Add(testGridPosition);
            }

        }


        return validGridPositions;

    }



}
