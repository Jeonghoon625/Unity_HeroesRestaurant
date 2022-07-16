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
         * 스킬 시전 중
         * ************************************************/
        if (skillManager.isSellectSkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                skillArea = Instantiate(skillManager.skillAreaPrefab);
                skillManager.isActiveSkill = true;
            }

            if(skillManager.isActiveSkill)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50f))
                {
                    var pos = Vector3.zero;
                    pos.x = hit.point.x;
                    skillArea.transform.position = pos;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    skillManager.CoqAuVin();
                    Destroy(skillArea);
                    skillManager.isActiveSkill = false;
                    skillManager.isSellectSkill = false;
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
