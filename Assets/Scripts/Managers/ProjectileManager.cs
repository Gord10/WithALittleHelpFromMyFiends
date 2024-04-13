using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Projectile projectilePrefab;
    public int poolSize;

    Projectile[] pool;

    private void Awake()
    {
        pool = new Projectile[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(projectilePrefab, transform);
            pool[i].gameObject.SetActive(false);
        }
    }

    public Projectile GetProjectileFromPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                return pool[i];
            }
        }

        return null;
    }

}
