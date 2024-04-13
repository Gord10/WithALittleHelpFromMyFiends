using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public static MobManager Instance => instance;
    static MobManager instance;

    public Mob mobPrefab;
    public int maxMobAmount = 100;

    Mob[] mobs;

    private void Awake()
    {
        instance = this;

        mobs = new Mob[maxMobAmount];
        Player player = FindObjectOfType<Player>();

        for(int i = 0; i < mobs.Length; i++)
        {
            float x = i * 2.5f;
            float y = 0;
            Vector3 pos = new Vector3(x - 20, y + 5, 0);
            mobs[i] = Instantiate(mobPrefab, pos, Quaternion.identity, transform);
            mobs[i].SetTarget(player);
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

        for(int i = 0;i < mobs.Length;i++)
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
}
