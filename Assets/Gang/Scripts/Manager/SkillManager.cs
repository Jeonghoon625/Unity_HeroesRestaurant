using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SkillParticle
{
    Ayran = 0,
    CoqAuVin = 1,
    CoqAuVinDown = 2,
    Fondue = 3,
    Limu = 4,
}
public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> heroSkills = new List<GameObject>();   // 스킬 이펙트
    private float yPosUp = 1.2f;
    public bool isSellectSkill = false;
    public bool isActiveSkill = false;
    public string taker;

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
            Instantiate(heroSkills[(int)SkillParticle.Ayran], pos, hero.transform.rotation).transform.parent = hero.transform;
        }
    }
    /******************************************
     * 코코뱅 스킬
     * ***************************************/
    public void OnClickCoqAuVinSkill()
    {
        SlowGame();
        taker = "CoqAuVin";
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            hero.doneMove = true;
        }
    }
    public void CoqAuVin(Vector3 skillPos)
    {
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            hero.doneMove = false;
            if (hero.name == "CoqAuVin")
            {
                hero.prevStateString = hero.curStateString;
                hero.animator.SetTrigger("Skill");
                hero.SetState("None");

                var pos = hero.transform.position;
                pos.y += yPosUp;
                Instantiate(heroSkills[(int)SkillParticle.CoqAuVin], pos, hero.transform.rotation).transform.parent = hero.transform;
            }
        }
        skillPos.y = 0f;
        skillPos.z = 0f;
        Instantiate(heroSkills[(int)SkillParticle.CoqAuVinDown], skillPos, new Quaternion(0f, 0f, 0f, 0f));
    }
    public void CoqAuVinAttack(List<GameObject> list)
    {
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            if (hero.name == "CoqAuVin")
            {
                foreach (var mob in list)
                {
                    mob.GetComponent<Enemy>().OnHit(hero, hero.Dmg * 3);
                }
            }
        }
    }
    /******************************************
     * 퐁듀 스킬
     * ***************************************/
    public void OnClickFondueSkill()
    {
        SlowGame();
        taker = "Fondue";
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            hero.doneMove = true;
        }
    }
    /******************************************
     * 리무 스킬
     * ***************************************/

    /******************************************
     * 범위 스킬
     * ***************************************/
    private void SlowGame()
    {
        isSellectSkill = true;
        Time.timeScale = 0.2f;
    }
    public void AreaSkill(string str, Vector3 skillPos)
    {
        switch (str)
        {
            case "CoqAuVin":
                CoqAuVin(skillPos);
                break;
        }

        Time.timeScale = 1f;
    }
}
