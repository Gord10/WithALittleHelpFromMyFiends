using CollectableItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendEnvy : FiendBase
    {
        public float range = 10;

        protected override void Awake()
        {
            base.Awake();
            SetTarget(Player.Instance);
        }

        private void FixedUpdate()
        {
            MoveTowardsTargetEnemy();
            //ChaseCollectableItem(range);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Collectable"))
            {
                if (collision.gameObject.TryGetComponent<CollectableBase>(out CollectableBase collectable))
                {
                    collectable.GetCollected(this);
                }
            }
        }

        protected override CollectableBase FindDesiredCollectableItem(float range)
        {
            return CollectableItemManager.Instance.GetClosestItem(Transform.position, range);
        }
    }
}

