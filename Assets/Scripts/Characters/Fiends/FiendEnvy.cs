using CollectableItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendEnvy : FiendBase
    {
        public float range = 10;

        private void FixedUpdate()
        {
            ChaseCollectableItem(range);
        }

        private void OnTriggerEnter2D(Collider2D collision)
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
            //HpPowerUp closestPowerUp = CollectableItemManager.Instance.GetClosestHpPowerUp(Transform.position, range);
            //Crystal closestCrystal = CollectableItemManager.Instance.GetClosestCrystal(Transform.position, range);

            //if(closestPowerUp && closestCrystal)
            //{
            //    return CollectableItemManager.Instance.GetClosestItem(Transform.position, range);
            //}

            //if(closestPowerUp)
            //{
            //    return closestPowerUp;
            //}

            //if(closestCrystal)
            //{
            //    return 
            //}

            //return null;

        }
    }
}

