using System.Collections;
using System.Collections.Generic;
using CollectableItem;
using Fiend;
using UnityEngine;
using UnityEngine.UIElements;

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
    public float slowDownCofactor = 0.5f; //Characters that get close to Sloth will slow down by this cofactor

    int collectedCrystals = 0; //Used for summoning fiends. Resets to 0 after summoning a fiend.
    GameCamera gameCamera;

    public enum State
    {
        IN_GAME,
        INTRODUCING_FIEND
    }

    public State state = State.IN_GAME;

    GameUi gameUi;
    Player player;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType(typeof(Player)) as Player;
        gameCamera = FindObjectOfType<GameCamera>();
        gameUi = FindObjectOfType<GameUi>();
        gameUi.SetXpBar(collectedCrystals, requiredCrystalToSummon);

        gameUi.ShowText($"Collect {requiredCrystalToSummon} crystals to summon a fiend");
    }

    public void OnCrystalCollection(Crystal crystal)
    {
        collectedCrystals += crystal.value;

        while(collectedCrystals >= requiredCrystalToSummon)
        {
            collectedCrystals -= requiredCrystalToSummon;
            Summon();
        }

        gameUi.SetXpBar(collectedCrystals, requiredCrystalToSummon);
    }

    void Summon()
    {
        state = State.INTRODUCING_FIEND;
        Time.timeScale = 0;
        FiendBase fiend = FiendManager.Instance.Summon();
        gameCamera.ShowFiend(fiend.Transform);
        gameUi.ShowText(fiend.description);
    }

    public void OnMobDeath(Mob mob)
    {
        if(!mob.IsSlowDown && CollectableItemManager.Instance.CanSpawnCrystal())
        {
            Crystal crystal = CollectableItemManager.Instance.GetCrystalFromPool();
            crystal.Spawn(mob.Transform.position);
        }

        MobManager.Instance.OnMobDeath();
    }

    public Vector3 GetRandomPointInWorld()
    {
        float x, y;
        float maxDistanceFromPlayer = 1;
        Vector3 position;

        //do
        //{
            x = Random.Range(-gameWorldHalfWidth, gameWorldHalfWidth);
            y = Random.Range(-gameWorldHalfHeight, gameWorldHalfHeight);
            position = new Vector3(x, y, 0);
        //}
        //while (Player.Instance.IsPointTooCloseToMe(position, maxDistanceFromPlayer));

        return position;
    }

    public static Vector3 GetRandomPointCloseToPoint(Vector3 point, float range)
    {
        Vector3 circle;
        Vector3 position;
        float maxDistanceFromPlayer = 0.6f;
        //We want to make sure that the random point is not too close to the player
        do
        {
            circle = Random.insideUnitCircle * range;
            position = point + circle;
        }
        while (Player.Instance.IsPointTooCloseToMe(position, maxDistanceFromPlayer)); 


        return point + circle;
    }

    public bool IsMovementAllowed()
    {
        return state == State.IN_GAME;
    }

    public void EndFiendIntroduction()
    {
        gameCamera.ShowPlayer(player.Transform);
        gameUi.HideText();
    }

    public void ContinueGame()
    {
        state = State.IN_GAME;
        Time.timeScale = 1;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && state == State.IN_GAME)
        {
            Summon();
        }
    }
#endif
}
