using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public SkillManager skillManager;
    private GameObject skillArea;
    private void Awake()
    {
        skillManager.isSellectSkill = false;
        skillManager.isActiveSkill = false;
    }
    private void Update()
    {
        /***************************************************
         * 스킬 시전 중 슬로우 모션
         * ************************************************/
        if (skillManager.isSellectSkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                skillArea = Instantiate(skillManager.skillAreaPrefab);
                skillManager.isActiveSkill = true;
            }

            if (skillManager.isActiveSkill)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                var pos = Vector3.zero;
                if (Physics.Raycast(ray, out hit, 50f))
                {
                    pos.x = hit.point.x;
                    skillArea.transform.position = pos;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    var skillAreaRender = skillArea.GetComponentsInChildren<SpriteRenderer>();
                    foreach(SpriteRenderer ren in skillAreaRender)
                    {
                        ren.enabled = false;
                    }
                    skillManager.AreaSkill(skillManager.taker, pos);
                    skillManager.isActiveSkill = false;
                    skillManager.isSellectSkill = false;

                    StartCoroutine(Delay());
                }
            }
        }
        /***************************************************
         * 스킬 시전 딜레이
         * ************************************************/
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);

            var cols = Physics.OverlapBox(skillArea.transform.position, skillArea.transform.localScale);
            List<GameObject> lists = new List<GameObject>();
            foreach (var col in cols)
            {
                if (col.gameObject.tag == "Monster")
                {
                    lists.Add(col.gameObject);
                }
            }
            //
            switch (skillManager.taker)
            {
                case "CoqAuVin":
                    skillManager.CoqAuVinAttack(lists);
                    Destroy(skillArea);
                    break;
            }
            yield break;
        }
        /***************************************************
         * 
         * ************************************************/
    }
}
