using CollectableItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendGreed : FiendBase
    {
        public float greediness = 4; //How much he wants to collect crystal

        private void FixedUpdate()
        {
            if(!targetedCollectable || !targetedCollectable.IsCollectable)
            {
                targetedCollectable = CollectableItemManager.Instance.GetClosestCrystal(Transform.position);
            }

            SearchForMobTarget();

            bool willChaseCrystal = WillChaseCollectable(greediness);

            if (willChaseCrystal)
            {
                MoveTowardsTargetCollectable();
            }
            else
            {
                MoveTowardsTargetEnemy();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Collectable"))
            {
                if (collision.gameObject.TryGetComponent<Crystal>(out Crystal collectable))
                {
                    collectable.GetCollected(this);
                }
            }
        }
    }
}

