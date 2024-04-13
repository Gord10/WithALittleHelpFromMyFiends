using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : NpcBase
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void Die()
    {
        base.Die();
        GameManager.Instance.OnMobDeath(this);
        gameObject.SetActive(false);
    }
}
