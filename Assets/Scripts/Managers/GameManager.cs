using System.Collections;
using System.Collections.Generic;
using CollectableItem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get 
        {
            if(!instance)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }
    static GameManager instance;

    public int requiredCrystalToSummon = 10;

    public float gameWorldHalfWidth = 50;
    public float gameWorldHalfHeight = 50;

    int collectedCrystals = 0; //Used for summoning fiends. Resets to 0 after summoning a fiend.

    GameUi gameUi;

    private void Awake()
    {
        instance = this;
        gameUi = FindObjectOfType<GameUi>();
        gameUi.SetXpBar(collectedCrystals, requiredCrystalToSummon);
    }

    public void OnCrystalCollection(Crystal crystal)
    {
        collectedCrystals += crystal.value;

        while(collectedCrystals >= requiredCrystalToSummon)
        {
            collectedCrystals -= requiredCrystalToSummon;
        }

        gameUi.SetXpBar(collectedCrystals, requiredCrystalToSummon);
    }

    public void OnMobDeath(Mob mob)
    {
        Crystal crystal = CollectableItemManager.Instance.GetCrystalFromPool();
        crystal.Spawn(mob.Transform.position);
    }

    public Vector3 GetRandomPointInWorld()
    {
        float x = Random.Range(-gameWorldHalfWidth, gameWorldHalfWidth);
        float y = Random.Range(-gameWorldHalfHeight, gameWorldHalfHeight);

        return new Vector3(x, y, 0);
    }

    public static Vector3 GetRandomPointCloseToPoint(Vector3 point, float range)
    {
        Vector3 circle = Random.insideUnitCircle * range;

        return point + circle;
    }
}
