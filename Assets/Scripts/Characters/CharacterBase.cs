using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float speed = 4;
    public float health = 10;

    protected Rigidbody2D rigidbody;
    protected Vector2 movementDirection;

    public Transform Transform => cachedTransform;
    Transform cachedTransform;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        cachedTransform = transform;
        movementDirection = new();
    }

    protected void SetDirectionTowardsTarget(Vector3 targetPosition)
    {
        Vector2 diff = targetPosition - cachedTransform.position;
        movementDirection = Vector2.ClampMagnitude(diff, 1);
    }

    public void MoveRigidbody()
    {
        rigidbody.velocity = movementDirection * speed;
    }

    public bool IsValidTarget()
    {
        return gameObject.activeSelf && health > 0;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
    }
}
