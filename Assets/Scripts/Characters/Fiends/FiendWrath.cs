using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiend
{
    public class FiendWrath : FiendBase
    {
        public Projectile projectilePrefab;
        public float projectileLaunchInterval = 0.6f;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(LaunchProjectileAtRandomTargets());
        }

        private void FixedUpdate()
        {
            SearchForMobTarget();
            MoveTowardsTargetEnemy();
        }

        IEnumerator LaunchProjectileAtRandomTargets()
        {
            WaitForSeconds wait = new WaitForSeconds(projectileLaunchInterval);
            while(true)
            {
                yield return wait;
                Mob mob = MobManager.Instance.GetRandomMobInRange(Transform.position, 10);
                Vector2 direction;
                if(mob != null)
                {
                    direction = mob.Transform.position - Transform.position;
                    direction.Normalize();
                }
                else
                {
                    direction = Random.insideUnitCircle.normalized;
                }

                Projectile projectile = Instantiate(projectilePrefab);
                projectile.Launch(Transform.position, direction);
            }
        }
    }
}

