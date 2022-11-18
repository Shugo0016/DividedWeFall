using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector3 targetPosition;
    private GridPosition gridPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }


    //Get grid position of unit and sets 
    private void Start()
    {
        //    print(transform.position);
        //    print(LevelGrid.Instance);
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    // Calls the move function to move unit from one space to the next currently does not work for grid
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


        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }

    // This function moves unit to position requested.
    public void Move(Vector3 targetPosition)
    {
        Vector3 movePosition = new Vector3(targetPosition.x, targetPosition.y + 1.5f, targetPosition.z);
        this.targetPosition = movePosition;
    }
}
