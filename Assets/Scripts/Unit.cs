using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector3 targetPosition;
    //private GridPosition gridPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }


    //Get grid position of unit and sets 
    private void Start()
    {
        GridPosition gridPosition = LevelSetup.Instance.GetGridPosition(transform.position);
        LevelSetup.Instance.SetUnitAtGridPosition(gridPosition, this);
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
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
