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

        protected void SearchForMobTarget()
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

        //Decides if fiend wants to chase an item like crystal or food, instead of an enemy. greedCofactor is how much he wants it
        protected bool WillChaseCollectable(float greedCofactor)
        {
            bool willChaseCollectable = targetedCollectable != null;
            //if (targetedCollectable && targetedEnemy)
            //{
            //    float distanceBetweenCrystal = Vector2.Distance(Transform.position, targetedCollectable.Transform.position);
            //    float distanceBetweenEnemy = Vector2.Distance(Transform.position, targetedEnemy.Transform.position);

            //    willChaseCollectable = distanceBetweenCrystal * greedCofactor < distanceBetweenEnemy;
            //}
            //else if (targetedCollectable)
            //{
            //    willChaseCollectable = true;
            //}
            //else
            //{
            //    willChaseCollectable = false;
            //}

            return willChaseCollectable;
        }
    }

}
