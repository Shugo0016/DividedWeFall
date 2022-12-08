using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrenadeProjectile : MonoBehaviour
{
    public static event EventHandler OnGrenadeExplosion;

    [SerializeField] private Transform grenadeHitEffectPrefab;
    [SerializeField] private AnimationCurve arcYAnimationCurve;
    private Vector3 targetPosition;

    private float totalDistance;
    private Vector3 positionXZ;

    private float damageRadius = 4f;

    private Action onGrenadeBehaviourComplete;

    private void Update()
    {
        Vector3 moveDir = (targetPosition - positionXZ).normalized;

        float moveSpeed = 15f;
        positionXZ += moveDir * moveSpeed * Time.deltaTime;

        float maxHeight = 9f;
        float distance = Vector3.Distance(positionXZ, targetPosition);
        float distanceNormalized = 1 - distance / totalDistance;

        float yPos = arcYAnimationCurve.Evaluate(distanceNormalized) * maxHeight;
        transform.position = new Vector3(positionXZ.x, yPos, positionXZ.z);

        float reachedTargetDistance = 0.2f;
        if (Vector3.Distance(positionXZ, targetPosition) < reachedTargetDistance)
        {
            Collider[] colliderArray = Physics.OverlapSphere(targetPosition, damageRadius);

            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.TakeDamage(30);
                }
            }

            OnGrenadeExplosion?.Invoke(this, EventArgs.Empty);
            Instantiate(grenadeHitEffectPrefab, targetPosition + Vector3.up * 1f, Quaternion.identity);
            Destroy(gameObject);

            onGrenadeBehaviourComplete();
        }
    }
    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete)
    {
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);

        positionXZ = transform.position;
        positionXZ.y = 0;
        totalDistance = Vector3.Distance(positionXZ, targetPosition);
    }
}
