using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public Image hpBar;
    public Image xpBar;

    public void SetHpBar(float hp, float maxHp)
    {
        hpBar.fillAmount = hp / maxHp;
    }

    public void SetXpBar(int crystalsNum, int requiredCrystalsNum)
    {
        xpBar.fillAmount = (float)crystalsNum / (float)requiredCrystalsNum;
    }
}
