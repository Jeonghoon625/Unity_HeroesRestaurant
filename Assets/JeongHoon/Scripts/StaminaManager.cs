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


    public void Show()
    {
        staminaText.text = (GameManager.Instance.goodsManager.stamina).ToString();
        maxStaminaText.text = "/" + maxStamina.ToString();
    }

    public void TimeStamina(double times)
    {
        maxStamina = GameManager.Instance.goodsManager.maxStaminaLV * 12;
        coolTime = coolTime - coolTime / 10 * (GameManager.Instance.goodsManager.coolTimeStaminaLV - 1);
        
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
}
