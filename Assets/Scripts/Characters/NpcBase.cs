using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectableItem;

public abstract class NpcBase : CharacterBase
{
    public float touchDamagePerSecond = 2;

    protected CharacterBase targetedEnemy;
    protected CollectableBase targetedCollectable;

    public void SetTarget(CharacterBase targetedEnemy)
    {
        this.targetedEnemy = targetedEnemy;
    }

    public void Spawn(Vector3 position)
    {
        health = MaxHealth;
        transform.position = position;
        rigidbody.velocity = Vector2.zero;
        movementDirection = Vector2.zero;
        gameObject.SetActive(true);
    }

    public void MoveTowardsTargetEnemy()
    {
        if (targetedEnemy != null)
        {
            MoveTowardsTargetPosition(targetedEnemy.Transform.position);
        }
    }

    public void MoveTowardsTargetCollectable()
    {
        if(targetedCollectable != null)
        {
            MoveTowardsTargetPosition(targetedCollectable.Transform.position);
        }
    }

    public void MoveTowardsTargetPosition(Vector3 position)
    {
        Vector3 targetPosition = position;
        SetDirectionTowardsTarget(targetPosition);
        MoveRigidbody();
    }

    public void ManualFixedUpdate()
    {
        MoveTowardsTargetEnemy();
    }
}
