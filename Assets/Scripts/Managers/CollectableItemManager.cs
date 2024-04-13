using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectableItem;

public class CollectableItemManager : MonoBehaviour
{
    public static CollectableItemManager Instance => instance;
    static CollectableItemManager instance;

    public int maxCrystals;
    Crystal[] crystals;

    HpPowerUp[] hpPowerUps;

    private void Awake()
    {
        instance = this;
        crystals = FindObjectsOfType<Crystal>();
        hpPowerUps = FindObjectsOfType<HpPowerUp>();
    }

    CollectableBase GetClosestCollectableInArray(CollectableBase[] array, Vector3 position, float range = float.MaxValue)
    {
        CollectableBase closestItem = null;
        float minDistance = range;

        for(int i = 0; i < array.Length; i++)
        {
            CollectableBase collectable = array[i];
            if (collectable.IsCollectable)
            {
                float distance = collectable.GetDistance(position);
                if (distance < minDistance)
                {
                    closestItem = collectable;
                    minDistance = distance;
                }
            }
        }

        return closestItem;
    }

    public Crystal GetClosestCrystal(Vector3 position, float range)
    {
        return GetClosestCollectableInArray(crystals, position, range) as Crystal;
    }

    public HpPowerUp GetClosestHpPowerUp(Vector3 position, float range)
    {
        return GetClosestCollectableInArray(hpPowerUps, position, range) as HpPowerUp;
    }

    public CollectableBase GetClosestItem(Vector3 position, float range)
    {
        Crystal closestCrystal = GetClosestCrystal(position, range);
        HpPowerUp closestHpPowerUp = GetClosestHpPowerUp(position, range);

        if(closestCrystal == null)
        {
            return closestHpPowerUp;
        }

        if(closestHpPowerUp == null)
        {
            return closestCrystal;
        }

        return ChooseClosestItem(closestHpPowerUp, closestCrystal, position);
    }

    public CollectableBase ChooseClosestItem(CollectableBase A, CollectableBase B, Vector3 point)
    {
        if(A.GetDistance(point) < B.GetDistance(point))
        {
            return A;
        }

        return B;
    }


}
