using Fiend;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public static MobManager Instance => instance;
    static MobManager instance;

    public float spawnInterval = 0.4f;
    public int mobStartNum = 20;
    public Mob mobPrefab;
    public int mobPoolSize = 100;

    Mob[] mobs;

    int mobsInSceneNum = 0;

    GameManager gameManager;

    private void Awake()
    {
        instance = this;
        gameManager = GameManager.Instance;
        mobs = new Mob[mobPoolSize];
        Player player = FindObjectOfType<Player>();

        for(int i = 0; i < mobs.Length; i++)
        {
            mobs[i] = Instantiate(mobPrefab, transform);
            mobs[i].SetTarget(player);
            mobs[i].gameObject.SetActive(false);
        }

        for(int i =0; i < mobStartNum; i++)
        {
            SpawnMob();
        }

        StartCoroutine(SpawnCoro());
    }

    IEnumerator SpawnCoro()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(spawnInterval);
        while(!gameManager.IsInEscapeState())
        {
            yield return waitForSeconds;
            if (mobsInSceneNum < GetMobLimit() && !gameManager.IsInEscapeState())
            {
                SpawnMob();
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            mobs[i].ManualFixedUpdate();
        }
    }

    public Mob GetClosestMobAlive(Vector3 pos)
    {
        float minDistance = float.MaxValue;
        Mob closestMob = null;

        for(int i = 0; i < mobs.Length;i++)
        {
            Mob mob = mobs[i];
            if(mob.IsValidTarget())
            {
                float distance = Vector2.Distance(pos, mobs[i].Transform.position);
                if (distance < minDistance)
                {
                    closestMob = mobs[i];
                    minDistance = distance;
                }
            }
        }

        return closestMob;
    }

    public void SpawnMob()
    {
        Mob mob = GetMobFromPool();
        if(mob)
        {
            Vector3 randomPoint = GameManager.Instance.GetRandomPointInWorld();
            mob.Spawn(randomPoint);
            mobsInSceneNum++;
        }
    }

    public void OnMobDeath()
    {
        mobsInSceneNum--;
    }

    Mob GetMobFromPool()
    {
        for(int i = 0; i <  mobs.Length; i++)
        {
            if (mobs[i] && !mobs[i].gameObject.activeSelf)
            {
                return mobs[i];
            }
        }

        return null;
    }

    public void KillAllMobs()
    {
        for(int i = 0;i < mobs.Length;i++)
        {
            mobs[i].Die();
        }
    }

    int GetMobLimit()
    {
        FiendBase nextFiend = FiendManager.Instance.GetFiendToSummon();
        if(nextFiend)
        {
            return nextFiend.maxMobNum;
        }

        return mobPoolSize;
    }

}
