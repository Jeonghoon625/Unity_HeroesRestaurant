using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaManager : MonoBehaviour
{
    private int maxStamina = 12;

    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI maxStaminaText;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI fulltimeText;

    private bool isInit = false;

    private float coolTime = 30f;

    private double timer = 0;

    private double remainTime = 0;

    private System.DateTime StartTime;
    private System.DateTime CurrentTime;

    public TextMeshProUGUI coolTimeText;
    public TextMeshProUGUI maxStaminaLVText;
    public TextMeshProUGUI coolTimeStaminaLVText;
    public TextMeshProUGUI maxStaminaCostText; //비용 텍스트
    public TextMeshProUGUI coolTimeStaminaCostText; //비용 텍스트
    public TextMeshProUGUI currentMaxStaminaLVInfoText;
    public TextMeshProUGUI nextMaxStaminaLVInfoText; 
    public TextMeshProUGUI currentCoolTimeStaminaInfoText; //이전 업글 정보
    public TextMeshProUGUI nextCoolTimeStaminaInfoText; //다음 업글 정보


    public void Show()
    {
        staminaText.text = (GameManager.Instance.goodsManager.stamina).ToString();
        maxStaminaText.text = "/" + maxStamina.ToString();
    }

    public void TimeStamina(double times)
    {
        maxStamina = GameManager.Instance.goodsManager.maxStaminaLV * 12;
        coolTime = 30 - 3 * (GameManager.Instance.goodsManager.coolTimeStaminaLV - 1);
        coolTimeText.text = coolTime.ToString() + "당 1회복";

        int sum = (int)(times / coolTime);
        remainTime = times % coolTime;

        if (sum >= 1)
        {
            if (sum + GameManager.Instance.goodsManager.stamina >= maxStamina)
            {
                GameManager.Instance.goodsManager.stamina = maxStamina;
                remainTime = 0;
            }
            else
            {
                GameManager.Instance.goodsManager.stamina += sum;
            }
        }

        int cost = 50 * GameManager.Instance.goodsManager.maxStaminaLV;

        maxStaminaLVText.text = "LV." + GameManager.Instance.goodsManager.maxStaminaLV;
        maxStaminaCostText.text = cost.ToString();
        currentMaxStaminaLVInfoText.text = "스태미나 최대 보유량 " + (100 * (GameManager.Instance.goodsManager.maxStaminaLV - 1)).ToString() + "%" + "증가";
        nextMaxStaminaLVInfoText.text = "다음 : " + "스태미나 최대 보유량 " + (100 * (GameManager.Instance.goodsManager.maxStaminaLV)).ToString() + "%" + "증가";

        cost = 50 * GameManager.Instance.goodsManager.coolTimeStaminaLV;

        coolTimeText.text = coolTime.ToString() + "당 1회복";
        coolTimeStaminaLVText.text = "LV." + GameManager.Instance.goodsManager.coolTimeStaminaLV;
        coolTimeStaminaCostText.text = cost.ToString();
        currentCoolTimeStaminaInfoText.text = "스태미나 충전속도 " + (100 * (GameManager.Instance.goodsManager.coolTimeStaminaLV - 1)).ToString() + "%" + "증가";
        nextCoolTimeStaminaInfoText.text = "다음 : " + "스태미나 충전속도 " + (100 * (GameManager.Instance.goodsManager.coolTimeStaminaLV)).ToString() + "%" + "증가";

        Show();

        StartTime = System.DateTime.Now;

        isInit = true;
    }

    public void Update()
    {
        if (isInit)
        {
            CurrentTime = System.DateTime.Now;
            System.TimeSpan timeCal = CurrentTime - StartTime;

            timer = timeCal.TotalSeconds;

            if (remainTime > 0)
            {
                timer += remainTime;
            }

            if (maxStamina != GameManager.Instance.goodsManager.stamina)
            {
                fulltimeText.text = "Time : " + (coolTime * (maxStamina - GameManager.Instance.goodsManager.stamina) - (int)timer).ToString() + "s";
                timeText.text = "개당 남은 시간 : " + (coolTime - (int)timer).ToString() + "s";
            }
            else
            {
                fulltimeText.text = "Full Charge";
                timeText.text = "";
                ResetTimer();
            }

            if (timer > coolTime)
            {
                if (GameManager.Instance.goodsManager.stamina >= maxStamina)
                {
                    GameManager.Instance.goodsManager.stamina = maxStamina;
                }
                else
                {
                    GameManager.Instance.goodsManager.stamina += 1;
                }

                Show();
                ResetTimer();
            }
        }
    }

    private void ResetTimer()
    {
        timer = 0;
        remainTime = 0;
        StartTime = System.DateTime.Now;
    }

    public void UpgradeMaxStamina()
    {
        int cost = 50 * GameManager.Instance.goodsManager.maxStaminaLV;

        if(GameManager.Instance.goodsManager.gold >= cost && GameManager.Instance.goodsManager.maxStaminaLV < 10)
        {
            GameManager.Instance.goodsManager.gold -= cost;
            GameManager.Instance.goodsManager.maxStaminaLV += 1;
            maxStamina = GameManager.Instance.goodsManager.maxStaminaLV * 12;
            maxStaminaLVText.text = "LV." + GameManager.Instance.goodsManager.maxStaminaLV;
            maxStaminaCostText.text = cost.ToString();
            currentMaxStaminaLVInfoText.text = "스태미나 최대 보유량 " + (100 * (GameManager.Instance.goodsManager.maxStaminaLV - 1)).ToString() + "%" + "증가";
            nextMaxStaminaLVInfoText.text = "다음 : " + "스태미나 최대 보유량 " + (100 * (GameManager.Instance.goodsManager.maxStaminaLV)).ToString() + "%" + "증가";
            Show();
        }
        else
        {
            return;
        }
    }

    public void UpgradeStaminaCoolTime()
    {
        int cost = 50 * GameManager.Instance.goodsManager.coolTimeStaminaLV;

        if (GameManager.Instance.goodsManager.gold >= cost && GameManager.Instance.goodsManager.coolTimeStaminaLV < 5)
        {
            GameManager.Instance.goodsManager.gold -= cost;
            GameManager.Instance.goodsManager.coolTimeStaminaLV += 1;
            coolTime = 30 - 3 * (GameManager.Instance.goodsManager.coolTimeStaminaLV - 1);
            coolTimeText.text = coolTime.ToString() + "당 1회복";
            coolTimeStaminaLVText.text = "LV." + GameManager.Instance.goodsManager.coolTimeStaminaLV;
            coolTimeStaminaCostText.text = cost.ToString();
            currentCoolTimeStaminaInfoText.text = "스태미나 충전속도 " + (100 * (GameManager.Instance.goodsManager.coolTimeStaminaLV - 1)).ToString() + "%" + "증가";
            nextCoolTimeStaminaInfoText.text = "다음 : " + "스태미나 충전속도 " + (100 * (GameManager.Instance.goodsManager.coolTimeStaminaLV)).ToString() + "%" + "증가";
        }
        else
        {
            return;
        }
    }
}
