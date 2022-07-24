using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image hpImage;
    public Image shieldImage;

    public void OnShield()
    {
        shieldImage.fillAmount = 1f;
        shieldImage.gameObject.SetActive(true);
    }

    public void OffShield()
    {
        shieldImage.gameObject.SetActive(false);
    }

    public void HitHp(float hp, float maxHp)
    {
        hpImage.fillAmount = hp / maxHp;
    }
    public float HitShield(float shield, float maxShield)
    {
        shieldImage.fillAmount = shield / maxShield;
        if (shieldImage.fillAmount <= 0f)
        {
            OffShield();
        }
        return shieldImage.fillAmount;
    }
}
