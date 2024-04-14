using DG.Tweening;
using Fiend;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public float speed = 4;
    public float health = 10;

    new protected Rigidbody2D rigidbody;
    protected Vector2 movementDirection;
    protected Animator animator;
    new protected Collider2D collider;

    protected GameManager gameManager;

    public Transform Transform
    {
        get
        {
            if(cachedTransform == null)
                cachedTransform = transform;
            return cachedTransform;
        }
    }

    Transform cachedTransform;

    float maxHealth; //Assigned at Awake
    public float MaxHealth => maxHealth;

    public bool IsSlowDown => isSlowDown;
    protected bool isSlowDown = false;

    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        cachedTransform = transform;
        movementDirection = new();
        maxHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        gameManager = GameManager.Instance;
    }

    protected void SetDirectionTowardsTarget(Vector3 targetPosition)
    {
        Vector2 diff = targetPosition - cachedTransform.position;
        movementDirection = Vector2.ClampMagnitude(diff, 1);
    }

    public void MoveRigidbody()
    {
        if(GameManager.Instance.IsMovementAllowed())
        {
            if (health > 0)
            {
                rigidbody.velocity = movementDirection * speed;

                if (movementDirection.x > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (movementDirection.x < 0)
                {
                    spriteRenderer.flipX = false;
                }

                if (isSlowDown)
                {
                    rigidbody.velocity *= GameManager.Instance.slowDownCofactor;
                }
            }
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (this is not FiendBase && collision.CompareTag("SlowDownArea"))
        {
            isSlowDown = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowDownArea"))
        {
            isSlowDown = false;
        }
    }

    public bool IsValidTarget()
    {
        return gameObject.activeSelf && health > 0;
    }

    public virtual void ReceiveDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void SetMovementDirectionWithInput()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        bool isMoving = movementX != 0 || movementY != 0;
        animator.speed = (isMoving) ? 2 : 1;

        movementDirection = new(movementX, movementY);
        movementDirection = Vector2.ClampMagnitude(movementDirection, 1);
    }

    protected virtual void OnDestroy()
    {
        spriteRenderer.DOKill();
    }

    public virtual void Die()
    {
        collider.enabled = false;
    }

    public float DistanceFromObject(Transform other)
    {
        return Vector2.Distance(Transform.position, other.position);
    }
}
