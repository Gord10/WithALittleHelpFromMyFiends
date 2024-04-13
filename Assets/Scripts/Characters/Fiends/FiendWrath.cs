using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendWrath : FiendBase
    {
        public float projectileLaunchInterval = 0.6f;
        ProjectileManager projectileManager;

        protected override void Awake()
        {
            base.Awake();

            projectileManager = FindObjectOfType<ProjectileManager>();
            StartCoroutine(LaunchProjectileAtRandomTargets());
        }

        private void FixedUpdate()
        {
            SearchForMobTarget();
            if(targetedEnemy && targetedEnemy is not Player && targetedEnemy.DistanceFromObject(Transform) > Player.Instance.DistanceFromObject(Transform))
            {
                SetTarget(Player.Instance);
            }

            MoveTowardsTargetEnemy();
        }

        IEnumerator LaunchProjectileAtRandomTargets()
        {
            WaitForSeconds wait = new WaitForSeconds(projectileLaunchInterval);
            while(true)
            {
                yield return wait;
                //Mob mob = MobManager.Instance.GetClosestMobAlive(Transform.position);
                Vector2 direction;
                if(targetedEnemy != null)
                {
                    direction = targetedEnemy.Transform.position - Transform.position;
                    direction.Normalize();
                }
                else
                {
                    direction = Random.insideUnitCircle.normalized;
                }

                Projectile projectile = projectileManager.GetProjectileFromPool();
                if (projectile)
                {
                    projectile.Launch(Transform.position, direction);
                }
                
            }
        }
    }
}

