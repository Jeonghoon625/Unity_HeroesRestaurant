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
    private List<GameObject> heroSkills = new List<GameObject>();   // Ω∫≈≥ ¿Ã∆Â∆Æ
    private float yPosUp = 1.2f;

    public void OnClickAyranSkill()
    {
        var heroList = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var heroInfo in heroList)
        {
            var hero = heroInfo.GetComponent<Heros>();

            if (hero.name == "Ayran")
            {
                hero.prevStateString = hero.curStateString;
                hero.SetState("Skill");
            }
            hero.isInvincibility = true;
            var pos = hero.transform.position;
            pos.y += yPosUp;
            Instantiate(heroSkills[(int)Skill.Ayran], pos, hero.transform.rotation).transform.parent = hero.transform;
        }
    }
}
