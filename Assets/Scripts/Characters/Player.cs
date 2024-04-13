using System.Collections;
using System.Collections.Generic;
using CollectableItem;
using UnityEngine;

public class Player : CharacterBase
{
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
        print("Player dies");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Collectable"))
        {
            if(collision.gameObject.TryGetComponent<Collectable>(out Collectable collectable))
            {
                collectable.GetCollected(this);
            }
        }
    }
}
