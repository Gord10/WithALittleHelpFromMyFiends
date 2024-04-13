using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendWrath : FiendBase
    {
        private void FixedUpdate()
        {
            SearchForMobTarget();
            MoveTowardsTargetEnemy();
        }
    }
}

