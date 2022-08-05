using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SkillParticle
{
    Ayran = 0,
    CoqAuVin = 1,
    CoqAuVinDown = 2,
    FondueSkill = 3,
    Fondue = 4,
    Limu = 5,
}
public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> heroSkills = new List<GameObject>();   // ��ų ����Ʈ
    private float yPosUp = 1.2f;
    public bool isSellectSkill = false;
    public bool isActiveSkill = false;
    public string taker;

    public GameObject skillAreaPrefab;
    /******************************************
     * �Ƹ��� ��ų
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
            pos.z += 0.2f;
            Instantiate(heroSkills[(int)SkillParticle.Ayran], pos, hero.transform.rotation).transform.parent = hero.transform;
        }
    }
    /******************************************
     * ���ڹ� ��ų
     * ***************************************/
    public void OnClickCoqAuVinSkill()
    {
        SlowGame();
        taker = "CoqAuVin";
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            hero.doneControll = true;
        }
    }
    public void CoqAuVin(Vector3 skillPos)
    {
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            hero.doneControll = false;
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
        Instantiate(heroSkills[(int)SkillParticle.CoqAuVinDown], skillPos, Quaternion.identity);
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
     * ���� ��ų
     * ***************************************/
    public void OnClickFondueSkill()
    {
        SlowGame();
        taker = "Fondue";
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            hero.doneControll = true;
        }
    }

    public void FondueSkill(Vector3 skillPos)
    {
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();
            hero.doneControll = false;
            if (hero.name == "Fondue")
            {
                hero.prevStateString = hero.curStateString;
                hero.animator.SetTrigger("Skill");
                hero.SetState("None");

                var pos = hero.transform.position;
                pos.y += yPosUp;
                Instantiate(heroSkills[(int)SkillParticle.Fondue], pos, hero.transform.rotation).transform.parent = hero.transform;
            }
        }
        skillPos.y = 0f;
        skillPos.z = 0f;
        Instantiate(heroSkills[(int)SkillParticle.FondueSkill], skillPos, Quaternion.identity);
    }
    /******************************************
     * ���� ��ų
     * ***************************************/
    public void OnClickLimuSkill()
    {
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();

            if (hero.name == "Limu")
            {
                hero.prevStateString = hero.curStateString;
                hero.animator.SetTrigger("Skill");
                hero.SetState("None");
            }
            hero.isShield = true;
            hero.curShield = hero.maxHp * 0.2f;
            hero.hpBar.GetComponent<HpBar>().OnShield();
            var pos = hero.transform.position;
            pos.y += yPosUp;
            pos.z += 0.2f;
            Instantiate(heroSkills[(int)SkillParticle.Limu], pos, hero.transform.rotation).transform.parent = hero.transform;
        }
    }
    /******************************************
     * ���� ��ų
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
            case "Fondue":
                FondueSkill(skillPos);
                break;
        }

        //Time.timeScale = 1f;
    }
}
