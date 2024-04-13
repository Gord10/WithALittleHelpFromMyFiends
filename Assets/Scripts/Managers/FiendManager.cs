using Fiend;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiendManager : MonoBehaviour
{
    public static FiendManager Instance => instance;
    static FiendManager instance;
    public FiendBase[] fiends;

    int fiendIndexToSummon = 0;

    private void Awake()
    {
        instance = this;
    }
    public void Summon()
    {
        FiendBase fiend = fiends[fiendIndexToSummon];
        float playerDistanceRange = 6;
        Vector3 pos = GameManager.GetRandomPointCloseToPoint(Player.Instance.Transform.position, playerDistanceRange);
        fiend.gameObject.SetActive(true);
        fiend.Spawn(pos);
        fiendIndexToSummon++;
    }
}
