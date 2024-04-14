using CollectableItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendGreed : FiendBase
    {
        public float range = 4;

        private void FixedUpdate()
        {
            if (gameManager.IsInEscapeState())
            {
                MoveToExit();
            }
            else
            {
                ChaseCollectableItem(range);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (collision.CompareTag("Collectable"))
            {
                if (collision.gameObject.TryGetComponent<Crystal>(out Crystal collectable))
                {
                    collectable.GetCollected(this);
                }
            }
        }

        protected override CollectableBase FindDesiredCollectableItem(float range)
        {
            return CollectableItemManager.Instance.GetClosestCrystal(Transform.position, range);
        }
    }
}

