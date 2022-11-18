using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 2;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;

    [SerializeField] private bool isEnemy;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
    }

    //Get grid position of unit and sets 
    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    // Calls the move function to move unit from one space to the next currently does not work for grid
    private void Update()
    {

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public bool GetIsEnemy()
    {
        return isEnemy;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        // TODO: set action points to the term
        if ((GetIsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) || (!GetIsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {

        }
    }
}
