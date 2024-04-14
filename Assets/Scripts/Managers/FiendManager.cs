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
    public int SummonedFiendNum => fiendIndexToSummon;

    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public FiendBase Summon()
    {
        audioSource.Play();
        FiendBase fiend = GetFiendToSummon();
        float playerDistanceRange = 1.2f;
        Vector3 pos = GameManager.GetRandomPointCloseToPoint(Player.Instance.Transform.position, playerDistanceRange);
 
        fiend.gameObject.SetActive(true);
        fiend.Spawn(pos);
        
        fiendIndexToSummon++;

        return fiend;
    }

    public FiendBase GetFiendToSummon()
    {
        if(fiendIndexToSummon < fiends.Length)
        {
            return fiends[fiendIndexToSummon];
        }

        return null;
    }

    public FiendBase GetClosestOtherFiend(FiendBase fiendSeekingOtherFiend)
    {
        FiendBase closestFiend = null;
        float minDistance = float.MaxValue;

        int i;
        for(i = 0; i < fiends.Length; i++)
        {
            if (fiends[i] != fiendSeekingOtherFiend && fiends[i].IsValidTarget())
            {
                float distance = fiends[i].DistanceFromObject(fiendSeekingOtherFiend.Transform);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    closestFiend = fiends[i];
                }
            }
        }

        return closestFiend;
    }

    public bool AreAllFiendsSummoned()
    {
        return fiendIndexToSummon >= fiends.Length;
    }

    public FiendBase GetLastFiend()
    {
        return fiends[^1];
    }
}
