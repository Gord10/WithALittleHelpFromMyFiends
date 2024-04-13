using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcBase : CharacterBase
{
    public float touchDamagePerSecond = 2;

    protected CharacterBase targetedEnemy;

    public void SetTarget(CharacterBase targetedEnemy)
    {
        this.targetedEnemy = targetedEnemy;
    }

    public void MoveTowardsTargetEnemy()
    {
        if (targetedEnemy != null)
        {
            Vector3 targetPosition = targetedEnemy.Transform.position;
            SetDirectionTowardsTarget(targetPosition);
            MoveRigidbody();
        }
    }

    public void ManualFixedUpdate()
    {
        MoveTowardsTargetEnemy();
    }
}
