using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothZone : MonoBehaviour
{
    public float fadeTime = 1;
    public float targetFadeAlpha = 0.7f;

    SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(targetFadeAlpha, fadeTime).SetLoops(-1, LoopType.Yoyo);
    }
}
