using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiend : NpcBase
{
    protected override void Awake()
    {
        base.Awake();
        damageableCharTags = new string[]{ "Mob", "Player"};
    }

    private void FixedUpdate()
    {
        if (targetedEnemy == null || !targetedEnemy.IsValidTarget())
        {
            Mob closestMob = MobManager.Instance.GetClosestMobAlive(Transform.position);
            targetedEnemy = closestMob;
        }

        ManualFixedUpdate();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(CanHarmThisCollider(collision.collider))
        {
            if(collision.gameObject.TryGetComponent(out CharacterBase character))
            {
                character.ReceiveDamage(touchDamagePerSecond * Time.fixedDeltaTime);
            }
        }
    }
}
