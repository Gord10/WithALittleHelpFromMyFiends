using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fiend;
using System;

public class GameCamera : MonoBehaviour
{
    public float movementSpeed = 1;
    float localZ;

    bool isWaitingForInputToEndFiendIntroduction = false;

    private void Awake()
    {
        localZ = transform.localPosition.z;
    }

    public void ShowFiend(Transform t)
    {
        transform.SetParent(t);
        transform.DOLocalMove(new Vector3(0, 0, localZ), movementSpeed).
            SetSpeedBased().
            SetUpdate(true).
            OnComplete(() => { isWaitingForInputToEndFiendIntroduction = true; });
    }

    public void ShowPlayer(Transform t)
    {
        transform.SetParent(t);
        transform.DOLocalMove(new Vector3(0, 0, localZ), movementSpeed).
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
