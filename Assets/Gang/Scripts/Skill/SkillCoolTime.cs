using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCoolTime : MonoBehaviour
{
    public Image skillFilter;
    public TextMeshProUGUI coolTimeCounter;
    private Button button;

    private float timer;
    [SerializeField]
    private float coolTime;

    private float currentCoolTime;

    private bool canUseSkill = true;

    private void Start()
    {
        skillFilter.fillAmount = 0;
        timer = coolTime;
        button = GetComponent<Button>();
        UseSkill();
    }
    public void UseSkill()
    {
        if(canUseSkill)
        {
            button.enabled = false;

            skillFilter.fillAmount = 1;
            StartCoroutine("Cooltime");

            timer = coolTime;
            currentCoolTime = timer;
            coolTimeCounter.text = "" + currentCoolTime;

            StartCoroutine("CoolTimeCounter");

            canUseSkill = false;
        }
    }

    IEnumerator Cooltime()
    {
        while(skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / timer;
            yield return null;
        }

        canUseSkill = true;

        yield break;
    }

    IEnumerator CoolTimeCounter()
    {
        while(currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
        }
        button.enabled = true;
        coolTimeCounter.text = "";
        yield break;
    }
}
