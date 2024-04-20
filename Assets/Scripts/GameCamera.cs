using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fiend;
using System;

public class GameCamera : MonoBehaviour
{
    public float movementSpeed = 1;
    public float shakeTime = 0.2f;
    public float shakeStrength = 0.2f;
    public int shakeVibrato = 10;
    
    Vector3 defaultLocalPos;

    bool isWaitingForInputToEndFiendIntroduction = false;
    bool isShaking = false;

    private void Awake()
    {
        defaultLocalPos = transform.localPosition;
    }

    public void ShowFiend(Transform t)
    {
        transform.DOKill();
        transform.SetParent(t);
        transform.DOLocalMove(defaultLocalPos, movementSpeed).
            SetSpeedBased().
            SetUpdate(true).
            OnComplete(() => { isWaitingForInputToEndFiendIntroduction = true; });
    }

    public void ShowPlayer(Transform t)
    {
        transform.DOKill();
        transform.SetParent(t);
        transform.DOLocalMove(defaultLocalPos, movementSpeed).
            SetSpeedBased().
            SetUpdate(true).
            OnComplete(() => { GameManager.Instance.ContinueGame(); });
    }

    private void Update()
    {
        if(isWaitingForInputToEndFiendIntroduction && Input.anyKeyDown)
        {
            GameManager.Instance.EndFiendIntroduction();
            isWaitingForInputToEndFiendIntroduction = false;
        }
    }

    public void StartShake()
    {
        if(!isShaking)
        {
            isShaking = true;
            transform.DOShakePosition(shakeTime, shakeStrength, shakeVibrato).OnComplete(() => { isShaking = false; });
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
