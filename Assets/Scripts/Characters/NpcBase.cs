using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectableItem;
using DG.Tweening;

public abstract class NpcBase : CharacterBase
{
    public float touchDamagePerSecond = 2;

    public float fadeOutTime = 1f;
    public float fadeInTime = 1f;

    protected CharacterBase targetedEnemy;
    protected CollectableBase targetedCollectable;

    protected Color defaultColor;

    protected override void Awake()
    {
        base.Awake();
        defaultColor = spriteRenderer.color;
    }

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
        Color color = defaultColor;
        color.a = 0;
        spriteRenderer.color = color;

        spriteRenderer.DOFade(1, fadeInTime).SetUpdate(true);

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

    public override void Die()
    {
        base.Die();
        animator.speed = 0;
        spriteRenderer.DOFade(0, fadeOutTime).OnComplete(() => { gameObject.SetActive(false); });
    }
}
