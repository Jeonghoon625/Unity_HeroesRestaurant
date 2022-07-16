using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCoolTime : MonoBehaviour
{
    //public TextMeshProUGUI text_CoolTime;
    //public Image image_fill;

    ////private 
    

    //void Start()
    //{
        
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T)) { StartCoroutine(CoolTime(3f)); }
    //}

    //IEnumerator CoolTime(float cool)
    //{
    //    print("쿨타임?코루틴?실행");
    //    while (cool > 1.0f)
    //    {
    //        cool -= Time.deltaTime;
    //        image_fill.fillAmount = (1.0f / cool);
    //        yield return new WaitForFixedUpdate(); 
    //    }
    //    print("쿨타임?코루틴?완료"); 
    //}

    public Image skillFilter;
    public TextMeshProUGUI coolTimeCounter;

    public float coolTime;

    private float currentCoolTime;

    private bool canUseSkill = true;

    private void Start()
    {
        skillFilter.fillAmount = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            UseSkill();
        }
    }

    public void UseSkill()
    {
        if(canUseSkill)
        {
            skillFilter.fillAmount = 1;
            StartCoroutine("Cooltime");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;

            StartCoroutine("CoolTimeCounter");

            canUseSkill = false;
        }
    }

    IEnumerator Cooltime()
    {
        while(skillFilter.fillAmount > 0)
        {
            //skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            skillFilter.fillAmount -= 1 * Time.deltaTime;
            Debug.Log(skillFilter.fillAmount);
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
        coolTimeCounter.text = "";
        yield break;
    }
}
