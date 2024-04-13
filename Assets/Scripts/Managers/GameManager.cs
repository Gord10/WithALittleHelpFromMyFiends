using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    static GameManager instance;

    public Player Player => player;
    Player player;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }

    public static Vector3 GetPlayerPosition()
    {
        return instance.Player.Transform.position;
    }
}
