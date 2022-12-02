using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 2;

    [SerializeField] private Animator unitAnimator;

    // To avoid a race condition between when the points are updated and 
    // when the onscreen text is updated
    public static event EventHandler OnAnyActionPointsChanged;

    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    private int actionPoints = ACTION_POINTS_MAX;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;

    private ShootAction shootAction;
    private BaseAction[] baseActionArray;

    private HealthSystem healthSystem;

    [SerializeField] private bool isEnemy;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        shootAction = GetComponent<ShootAction>();
        baseActionArray = GetComponents<BaseAction>();
    }

    //Get grid position of unit and sets 
    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        healthSystem.OnDead += HealthSystem_OnDead;

        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    // Calls the move function to move unit from one space to the next currently does not work for grid
    private void Update()
    {
        
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            
            LevelGrid.Instance.UnitMovedPosition(this, gridPosition, newGridPosition);
            
            
            gridPosition = newGridPosition;
            while (gridPosition != newGridPosition)
            {
                unitAnimator.SetBool("isWalking", true);
            }
                 

        }
        else
        {
            unitAnimator.SetBool("isWalking", false);
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

    public ShootAction GetShootAction()
    {
        return shootAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
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
            actionPoints = ACTION_POINTS_MAX;

            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        return false;
    }
    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (actionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    // function to take Damage
    public void TakeDamage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        // removes the unit from the grid position
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);

        // destroys the unit
        Destroy(gameObject);

        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return healthSystem.GetHealthNormalized();
    }
}
