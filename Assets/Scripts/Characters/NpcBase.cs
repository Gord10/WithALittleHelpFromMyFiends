using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBase : CharacterBase
{
    public float touchDamagePerSecond = 2;

    protected CharacterBase targetedEnemy;

    protected string[] damageableCharTags;

    public void SetTarget(CharacterBase targetedEnemy)
    {
        this.targetedEnemy = targetedEnemy;
    }

    public void ManualFixedUpdate()
    {
        Vector3 targetPosition = targetedEnemy.Transform.position;
        SetDirectionTowardsTarget(targetPosition);
        MoveRigidbody();
    }

    public bool CanHarmThisCollider(Collider2D collider)
    {
        for(int i = 0; i < damageableCharTags.Length; i++)
        {
            if (collider.CompareTag(damageableCharTags[i]))
            {
                return true;
            }
        }

        return false;
    }
}
