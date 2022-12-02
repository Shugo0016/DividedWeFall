using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootAction : BaseAction
{
    // use a state machine for the animation
    public event EventHandler <OnShootEventArgs> onShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit;

    }

    private enum State
    {
        Aim,
        Shoot,
        Cooldown,
    }
    private float totalSpinAmount;

    // max distance till which the player can select an enemy to shoot at
    private int maxShootDistance = 10;

    private State state;

    private float timer;

    // this stores the target unit which is being fired upon
    private Unit targetUnit;

    // indicates whether the player can shoot or not
    private bool canShoot;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        timer -= Time.deltaTime;

        // performs the different actions based on the state of the character
        switch (state)
        {
            case State.Aim:
                Vector3 aimDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * rotateSpeed);
                break;
            case State.Shoot:
                if (canShoot)
                {
                    Shoot();
                    canShoot = false;
                }
                break;
            case State.Cooldown:
                break;
        }

        // switches to the next state once the timer hits 0
        if (timer <= 0f)
        {
            SwitchState();
        }

    }

    // switches to the next state 
    private void SwitchState()
    {
        switch (state)
        {
            case State.Aim:
                state = State.Shoot;
                float shootTime = 0.1f;
                timer = shootTime;
                break;
            case State.Shoot:
                state = State.Cooldown;
                float cooldownTime = 0.1f;
                timer = cooldownTime;
                break;
            case State.Cooldown:
                ActionComplete();
                break;
        }

        // Debug.Log(state);
    }
    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return GetValidActionGridList(unitGridPosition);
    }

    // checks for any enemy units within the specified maxShootDistance
    public List<GridPosition> GetValidActionGridList(GridPosition unitGridPosition)
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                // restricts the grid to a rhombus shape
                if ((Math.Abs(unitGridPosition.x - testGridPosition.x) + Math.Abs(unitGridPosition.z - testGridPosition.z)) > maxShootDistance)
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //GridPosition Occupied by another unit
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                // we don't need to check if the targetUnit is null since we have a check for a unit being on this 
                // grid position before it

                // checks if both the units are on the same team and modifies the units accordingly
                if (targetUnit.GetIsEnemy() == unit.GetIsEnemy())
                {
                    continue;
                }

                // Debug.Log(testGridPosition);
                validGridPositions.Add(testGridPosition);
            }

        }


        return validGridPositions;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        // switches to the first state and initializes the timer
        state = State.Aim;
        float aimTime = 1f;
        timer = aimTime;

        // allows the player to shoot a single bullet
        canShoot = true;
    }

    private void Shoot()
    {
        onShoot?.Invoke(this, new OnShootEventArgs
        {
            targetUnit = targetUnit,
            shootingUnit = unit
        });
        targetUnit.TakeDamage(40);

    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        Unit target = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 100 + Mathf.RoundToInt((1 - target.GetHealthNormalized()) * 100f),
        };
    }

    public int GetTargetCountAtPosition(GridPosition gridPosition)
    {
        return GetValidActionGridList(gridPosition).Count;
    }
}
