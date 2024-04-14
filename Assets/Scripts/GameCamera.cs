using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fiend;
using System;

public class GameCamera : MonoBehaviour
{
    public float movementSpeed = 1;
    Vector3 defaultLocalPos;

    bool isWaitingForInputToEndFiendIntroduction = false;

    private void Awake()
    {
        defaultLocalPos = transform.localPosition;
    }

    public void ShowFiend(Transform t)
    {
        transform.SetParent(t);
        transform.DOLocalMove(defaultLocalPos, movementSpeed).
            SetSpeedBased().
            SetUpdate(true).
            OnComplete(() => { isWaitingForInputToEndFiendIntroduction = true; });
    }

    public void ShowPlayer(Transform t)
    {
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
}
