using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendBase : NpcBase
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected void SearchForTarget()
        {
            if (targetedEnemy == null || !targetedEnemy.IsValidTarget())
            {
                Mob closestMob = MobManager.Instance.GetClosestMobAlive(Transform.position);
                SetTarget(closestMob);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Mob") || collision.collider.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent(out CharacterBase character))
                {
                    character.ReceiveDamage(touchDamagePerSecond * Time.fixedDeltaTime);
                }
            }
        }
    }

}
