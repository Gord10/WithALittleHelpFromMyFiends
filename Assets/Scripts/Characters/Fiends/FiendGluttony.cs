using CollectableItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendGluttony: FiendBase
    {
        public float range = 10;

        private void FixedUpdate()
        {
            ChaseCollectableItem(range);
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Collectable"))
            {
                if (collision.gameObject.TryGetComponent<HpPowerUp>(out HpPowerUp hpPowerUp))
                {
                    hpPowerUp.GetCollected(this);
                }
            }
        }

        protected override CollectableBase FindDesiredCollectableItem(float range)
        {
            return CollectableItemManager.Instance.GetClosestHpPowerUp(Transform.position, range);
        }
    }
}

