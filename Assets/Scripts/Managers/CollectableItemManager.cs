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

    private void Awake()
    {
        instance = this;
        crystals = FindObjectsOfType<Crystal>();
    }

    public CollectableBase GetClosestCollectable(CollectableBase[] array, Vector3 position)
    {
        CollectableBase closestItem = null;
        float minDistance = float.MaxValue;

        for(int i = 0; i < array.Length; i++)
        {
            CollectableBase collectable = array[i];
            if (collectable.IsCollectable)
            {
                float distance = Vector2.Distance(collectable.Transform.position, position);
                if (distance < minDistance)
                {
                    closestItem = collectable;
                    minDistance = distance;
                }
            }
        }

        return closestItem;
    }

    public Crystal GetClosestCrystal(Vector3 position)
    {
        return GetClosestCollectable(crystals, position) as Crystal;
    }
}
