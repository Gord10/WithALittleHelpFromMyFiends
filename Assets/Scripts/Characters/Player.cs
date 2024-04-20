using System.Collections;
using System.Collections.Generic;
using CollectableItem;
using UnityEngine;

public class Player : CharacterBase
{
    public static Player Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }
    private static Player instance;

    GameUi gameUi;
    GameCamera gameCamera;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        gameUi = FindObjectOfType<GameUi>();
        gameUi.SetHpBar(health, MaxHealth, false);

        gameCamera = FindObjectOfType<GameCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMovementDirectionWithInput();
    }

    private void FixedUpdate()
    {
        MoveRigidbody();
    }

    public override void Die()
    {
        base.Die();
        GameManager.RestartLevel();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Mob"))
        {
            if(collision.collider.TryGetComponent<NpcBase>(out NpcBase npc))
            {
                ReceiveDamage(npc.touchDamagePerSecond * Time.fixedDeltaTime);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.CompareTag("Collectable"))
        {
            if(collision.gameObject.TryGetComponent<CollectableBase>(out CollectableBase collectable))
            {
                collectable.GetCollected(this);
            }
        }

        if(collision.CompareTag("Exit"))
        {
            if(gameManager.IsInEscapeState())
            {
                gameManager.OnPlayerEscape();
            }
            else
            {
                gameUi.ShowExitText();
            }
            
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D (other);
        if (other.CompareTag("Exit"))
        {
            gameUi.HideExitText();
        }
    }

    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        bool willShowTween = damage > 0.1f;
        gameUi.SetHpBar(health, MaxHealth, willShowTween);
        gameCamera.StartShake();
    }

    public void IncreaseHealth(float increase)
    {
        health += increase;
        health = Mathf.Clamp(health, 0, MaxHealth);
        gameUi.SetHpBar(health, MaxHealth, true);
    }

    public bool IsPointTooCloseToMe(Vector3 point, float maxDistance)
    {
        return Vector2.Distance(Transform.position, point) <= maxDistance;
    }
}
