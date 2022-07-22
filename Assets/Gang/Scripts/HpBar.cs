using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image hpImage;
    public Image shieldImage;

    private GameObject obj;
    private void Start()
    {
        obj = GetComponentInParent<Heros>().gameObject;
    }
    void Update()
    {
        //gameObject.transform.rotation = Camera.main.transform.rotation;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnShield();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OffShield();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log(obj);
        }
    }

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
