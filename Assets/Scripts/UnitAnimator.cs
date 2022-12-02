using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;

        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.onShoot += ShootAction_OnShoot;
        }

    }

    private void MoveAction_OnStartMoving(object sender, System.EventArgs e)
    {
        animator.SetBool("isWalking", true);
    }

    private void MoveAction_OnStopMoving(object sender, System.EventArgs e)
    {
        animator.SetBool("isWalking", false);
    }

    private void ShootAction_OnShoot(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Shoot");
    }


}
    
