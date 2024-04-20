using CollectableItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendEnvy : FiendBase
    {
        protected override void Awake()
        {
            base.Awake();
            SetTarget(Player.Instance);
        }

        private void FixedUpdate()
        {
            MoveTowardsTargetEnemy();
        }
    }
}

