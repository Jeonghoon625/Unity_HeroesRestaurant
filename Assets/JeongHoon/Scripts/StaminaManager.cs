using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaManager : MonoBehaviour
{
    private int maxStamina;

    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI maxStaminaText;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI fulltimeText;

    private bool isInit = false;

    private float coolTime = 60f;

    private float timer = 0;

    public void Show()
    {
        staminaText.text = (GameManager.Instance.goodsManager.stamina).ToString();
        maxStaminaText.text = (GameManager.Instance.masterStage * 12).ToString();
    }

    public void TimeStamina(double times)
    {
        maxStamina = (GameManager.Instance.masterStage * 12);

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

        isInit = true;
    }

    public void Update()
    {
        if(isInit)
        {
            timer += Time.deltaTime;

            if(timer > coolTime)
            {
                if (GameManager.Instance.goodsManager.stamina >= maxStamina)
                {
                    GameManager.Instance.goodsManager.stamina = maxStamina;
                }
                else
                {
                    GameManager.Instance.goodsManager.stamina += 1;
                }
                    
                timer = 0;
            }
        }
    }
}
