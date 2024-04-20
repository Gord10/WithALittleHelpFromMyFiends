using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class  FiendLust : FiendBase
    {
        private void FixedUpdate()
        {
            if (gameManager.IsInEscapeState()) //He understood he is rejected by player. A rejection he can't take.
            {
                SetTarget(Player.Instance);
                MoveTowardsTargetEnemy();
                return;
            }

            Vector3 playerPos = Player.Instance.Transform.position;
            Mob mobClosestToPlayer = MobManager.Instance.GetClosestMobAlive(playerPos);

            if (mobClosestToPlayer != null)
            {
                SetTarget(mobClosestToPlayer);
                MoveTowardsTargetEnemy();
            }

            //if(targetedEnemy && !targetedEnemy.IsValidTarget())
            //{
            //    targetedEnemy = null;
            //}

            //if(!targetedEnemy)
            //{
            //    Vector3 playerPos = Player.Instance.Transform.position;
            //    Mob mobClosestToPlayer = MobManager.Instance.GetClosestMobAlive(playerPos);

            //    if(mobClosestToPlayer != null)
            //    {
            //        SetTarget(mobClosestToPlayer);
            //    }
            //}


        }
    }
}