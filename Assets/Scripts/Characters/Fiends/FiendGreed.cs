using CollectableItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendGreed : FiendBase
    {
        public float range = 4; //How much he wants to collect crystal

        private void FixedUpdate()
        {
            ChaseCollectableItem(range);
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

        protected override CollectableBase FindDesiredCollectableItem(float range)
        {
            return CollectableItemManager.Instance.GetClosestCrystal(Transform.position, range);
        }
    }
}
