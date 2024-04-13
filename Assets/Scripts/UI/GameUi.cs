using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUi : MonoBehaviour
{
    public Image hpBar;
    public Image xpBar;
    public float fillTweenSpeed = 1;
    public void SetHpBar(float hp, float maxHp)
    {
        float fillAmount = hp / maxHp;
        hpBar.fillAmount = fillAmount;
    }

    public void SetXpBar(int crystalsNum, int requiredCrystalsNum)
    {
        float fillAmount = (float)crystalsNum / (float)requiredCrystalsNum;
        xpBar.DOKill();

        if(fillAmount == 0)
        {
            xpBar.fillAmount = 0;
        }
        else
        {
            xpBar.DOFillAmount(fillAmount, fillTweenSpeed).SetSpeedBased();
        }
    }
}
