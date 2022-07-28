using System.Collections;
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

    private float timer = 0;

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
        
        if(sum >= 1)
        {
            if (sum + GameManager.Instance.goodsManager.stamina >= maxStamina)
            {
                GameManager.Instance.goodsManager.stamina = maxStamina;
            }
            else
            {
                GameManager.Instance.goodsManager.stamina += sum;
            }
        }

        Show();
        isInit = true;
    }

    public void Update()
    {
        if(isInit)
        {
            timer += Time.deltaTime;
            timeText.text = "Time : " + (coolTime * maxStamina - (int)timer).ToString() + "s";

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
                timer = 0;
            }
        }
    }
}
