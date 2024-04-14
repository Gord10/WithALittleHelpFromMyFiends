using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameUi : MonoBehaviour
{
    public Image hpBar;
    public Image xpBar;
    public float fillTweenSpeed = 1;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI exitText;
    public TextMeshProUGUI fiendCounterText;
    public GameObject attackInfoText;

    static bool didWeShowAttackInfoText = false;

    private void Awake()
    {
        exitText.gameObject.SetActive(false);
        fiendCounterText.gameObject.SetActive(false);
        attackInfoText.gameObject.SetActive(false);
    }

    public void SetHpBar(float hp, float maxHp, bool willUseTween)
    {
        float fillAmount = hp / maxHp;

        if(willUseTween)
        {
            hpBar.DOKill();
            hpBar.DOFillAmount(fillAmount, fillTweenSpeed).SetSpeedBased();
        }
        else
        {
            hpBar.fillAmount = fillAmount;
        }
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

    public void ShowText(string text)
    {
        infoText.text = text;
        infoText.gameObject.SetActive(true);
    }

    public void HideText()
    {
        infoText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        xpBar.DOKill();
        hpBar.DOKill();
    }

    public void ShowExitText()
    {
        exitText.gameObject.SetActive(true);
    }

    public void HideExitText()
    {
        print("Hide exit text");
        if(exitText != null)
        {
            exitText.gameObject.SetActive(false);
        }
    }

    public void UpdateFiendCounter(int summonedAmount)
    {
        fiendCounterText.text = $"Fiends:\n{summonedAmount.ToString()}/7";
        fiendCounterText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !didWeShowAttackInfoText)
        {
            attackInfoText.SetActive(true);
            didWeShowAttackInfoText = true;
            StartCoroutine(DisableAttackInfoText(3));
        }
    }

    IEnumerator DisableAttackInfoText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        attackInfoText.SetActive(false);
    }
}
