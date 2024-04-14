using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class  FiendLust : FiendBase
    {
        private void FixedUpdate()
        {
            if (gameManager.IsInEscapeState())
            {
                MoveToExit();
                return;
            }

            SearchForMobTarget();
            MoveTowardsTargetEnemy();
        }
    }
}