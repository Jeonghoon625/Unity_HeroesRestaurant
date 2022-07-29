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
    public TextMeshProUGUI maxStaminaCostText; //��� �ؽ�Ʈ
    public TextMeshProUGUI coolTimeStaminaCostText; //��� �ؽ�Ʈ
    public TextMeshProUGUI currentMaxStaminaLVInfoText;
    public TextMeshProUGUI nextMaxStaminaLVInfoText; 
    public TextMeshProUGUI currentCoolTimeStaminaInfoText; //���� ���� ����
    public TextMeshProUGUI nextCoolTimeStaminaInfoText; //���� ���� ����


    public void Show()
    {
        staminaText.text = (GameManager.Instance.goodsManager.stamina).ToString();
        maxStaminaText.text = "/" + maxStamina.ToString();
    }

    public void TimeStamina(double times)
    {
        maxStamina = GameManager.Instance.goodsManager.maxStaminaLV * 12;
        coolTime = 30 - 3 * (GameManager.Instance.goodsManager.coolTimeStaminaLV - 1);
        coolTimeText.text = coolTime.ToString() + "�� 1ȸ��";

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
        currentMaxStaminaLVInfoText.text = "���¹̳� �ִ� ������ " + (100 * (GameManager.Instance.goodsManager.maxStaminaLV - 1)).ToString() + "%" + "����";
        nextMaxStaminaLVInfoText.text = "���� : " + "���¹̳� �ִ� ������ " + (100 * (GameManager.Instance.goodsManager.maxStaminaLV)).ToString() + "%" + "����";

        cost = 50 * GameManager.Instance.goodsManager.coolTimeStaminaLV;

        coolTimeText.text = coolTime.ToString() + "�� 1ȸ��";
        coolTimeStaminaLVText.text = "LV." + GameManager.Instance.goodsManager.coolTimeStaminaLV;
        coolTimeStaminaCostText.text = cost.ToString();
        currentCoolTimeStaminaInfoText.text = "���¹̳� �����ӵ� " + (100 * (GameManager.Instance.goodsManager.coolTimeStaminaLV - 1)).ToString() + "%" + "����";
        nextCoolTimeStaminaInfoText.text = "���� : " + "���¹̳� �����ӵ� " + (100 * (GameManager.Instance.goodsManager.coolTimeStaminaLV)).ToString() + "%" + "����";

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
                timeText.text = "���� ���� �ð� : " + (coolTime - (int)timer).ToString() + "s";
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
            currentMaxStaminaLVInfoText.text = "���¹̳� �ִ� ������ " + (100 * (GameManager.Instance.goodsManager.maxStaminaLV - 1)).ToString() + "%" + "����";
            nextMaxStaminaLVInfoText.text = "���� : " + "���¹̳� �ִ� ������ " + (100 * (GameManager.Instance.goodsManager.maxStaminaLV)).ToString() + "%" + "����";
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
            coolTimeText.text = coolTime.ToString() + "�� 1ȸ��";
            coolTimeStaminaLVText.text = "LV." + GameManager.Instance.goodsManager.coolTimeStaminaLV;
            coolTimeStaminaCostText.text = cost.ToString();
            currentCoolTimeStaminaInfoText.text = "���¹̳� �����ӵ� " + (100 * (GameManager.Instance.goodsManager.coolTimeStaminaLV - 1)).ToString() + "%" + "����";
            nextCoolTimeStaminaInfoText.text = "���� : " + "���¹̳� �����ӵ� " + (100 * (GameManager.Instance.goodsManager.coolTimeStaminaLV)).ToString() + "%" + "����";
        }
        else
        {
            return;
        }
    }
}
