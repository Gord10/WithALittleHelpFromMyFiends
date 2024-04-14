using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectableItem;
using DG.Tweening;

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

        if(rigidbody != null)
        {
            rigidbody.velocity = Vector2.zero;
        }
        
        movementDirection = Vector2.zero;
        gameObject.SetActive(true);
        
        animator.speed = 1;
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        float spawnFadeInTime = 2;
        spriteRenderer.DOFade(1, spawnFadeInTime).SetUpdate(true);

        isSlowDown = false;

        if(collider != null)
        {
            collider.enabled = true;
        }
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
        MoveTowardsTargetPosition(targetedCollectable.Transform.position);
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
