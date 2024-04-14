using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectableItem;

namespace Fiend
{
    public class FiendBase : NpcBase
    {
        public string description;
        public bool doesWantToDamagePlayer = false;
        public bool doesWantToDamageMobs = true;
        public int maxMobNum = 10;

        protected Exit exit;

        protected override void Awake()
        {
            base.Awake();
            exit = FindObjectOfType<Exit>();
        }

        protected void SearchForMobTarget()
        {
            Mob closestMob = MobManager.Instance.GetClosestMobAlive(Transform.position);
            SetTarget(closestMob);
        }

        protected void MoveToExit()
        {
            MoveTowardsTargetPosition(exit.transform.position);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (touchDamagePerSecond > 0 && CanHarmThisCollider(collision.collider))
            {
                if (collision.gameObject.TryGetComponent(out CharacterBase character))
                {
                    character.ReceiveDamage(touchDamagePerSecond * Time.fixedDeltaTime);
                }
            }
        }

        //Decides if fiend wants to chase an item like crystal or food, instead of an enemy
        protected bool WillChaseCollectable()
        {
            bool willChaseCollectable = targetedCollectable != null && targetedCollectable.IsCollectable;
            return willChaseCollectable;
        }

        protected virtual CollectableBase FindDesiredCollectableItem(float range)
        {
            return null;
        }

        protected void ChaseCollectableItem(float range)
        {
            targetedCollectable = FindDesiredCollectableItem(range);

            if (doesWantToDamageMobs)
            {
                SearchForMobTarget();
            }
            else if(doesWantToDamagePlayer && targetedEnemy == null)
            {
                SetTarget(Player.Instance);
            }

            bool willChaseCrystal = WillChaseCollectable();

            if (willChaseCrystal)
            {
                MoveTowardsTargetCollectable();
                //print($"Chase collectable {targetedCollectable.gameObject.name}");
            }
            else
            {
                MoveTowardsTargetEnemy();
            }
        }

        protected virtual bool CanHarmThisCollider(Collider2D collider)
        {
            return (collider.CompareTag("Mob") || collider.CompareTag("Player"));
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if(collision.CompareTag("Exit") && gameManager.IsInEscapeState() && this is not FiendPride)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
