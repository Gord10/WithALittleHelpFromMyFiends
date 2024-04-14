using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mob : NpcBase
{
    public float fadeOutTime = 1f;

    protected override void Awake()
    {
        base.Awake();
    }
    public override void Die()
    {
        base.Die();
        GameManager.Instance.OnMobDeath(this);
        animator.speed = 0;
        spriteRenderer.DOFade(0, fadeOutTime).OnComplete(() => { gameObject.SetActive(false); });
        //gameObject.SetActive(false);
    }
}
