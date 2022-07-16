using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Skill
{
    Ayran = 0,
    CoqAuVin = 1,
    Fondue = 2,
    Limu = 3,
}
public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> heroSkills = new List<GameObject>();   // 스킬 이펙트
    private float yPosUp = 1.2f;
    public bool isSellectSkill = false;
    public bool isActiveSkill = false;

    public GameObject skillAreaPrefab;
    /******************************************
     * 아리안 스킬
     * ***************************************/
    public void OnClickAyranSkill()
    {
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();

            if (hero.name == "Ayran")
            {
                hero.prevStateString = hero.curStateString;
                hero.animator.SetTrigger("Skill");
                hero.SetState("None");
            }
            hero.isInvincibility = true;
            var pos = hero.transform.position;
            pos.y += yPosUp;
            Instantiate(heroSkills[(int)Skill.Ayran], pos, hero.transform.rotation).transform.parent = hero.transform;
        }
    }
    /******************************************
     * 코코뱅 스킬
     * ***************************************/
    public void OnClickCoqAuVinSkill()
    {
        SlowGame();
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();

            hero.isActiveSkill = true;
        }
    }
    public void CoqAuVin()
    {
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            hero.isActiveSkill = false;
            if (hero.name == "CoqAuVin")
            {
                hero.prevStateString = hero.curStateString;
                hero.animator.SetTrigger("Skill");
                hero.SetState("None");
            }
        }
    }
    private void SlowGame()
    {
        isSellectSkill = true;
        Time.timeScale = 0.2f;
    }
}
