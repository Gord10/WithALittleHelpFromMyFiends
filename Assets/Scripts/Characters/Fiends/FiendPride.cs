using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendPride : FiendBase
    {
        private void FixedUpdate()
        {
            FiendBase targetFiend = FiendManager.Instance.GetClosestOtherFiend(this);
            if(targetFiend != null)
            {
                SetTarget(targetFiend);
            }
            else
            {
                SetTarget(Player.Instance);
            }

            MoveTowardsTargetEnemy();

        }

        protected override bool CanHarmThisCollider(Collider2D collider)
        {
            return (collider.CompareTag("Mob") || collider.CompareTag("Player") || collider.CompareTag("Fiend"));
        }
    }
}

