using System.Collections;
using System.Collections.Generic;
using CollectableItem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    static GameManager instance;

    public Player Player => player;
    Player player;

    public int requiredCrystalToSummon = 10;

    int collectedCrystals = 0; //Used for summoning fiends. Resets to 0 after summoning a fiend.

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }

    public static Vector3 GetPlayerPosition()
    {
        return instance.Player.Transform.position;
    }

    public void OnCrystalCollection(Crystal crystal)
    {
        collectedCrystals += crystal.value;
    }
}
