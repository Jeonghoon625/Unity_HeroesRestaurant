using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    /******************************************
     * 스킬 관리
     * ***************************************/
    public SkillManager skillManager;
    private GameObject skillArea;
    /******************************************
     * 스테이지 성공, 실패 여부 확인
     * ***************************************/
    private bool stageEnd = false;
    public int HeroCount;
    public GameObject VictoryUI;
    public GameObject DefeatUI;

    private HeroList heroList;
    private void Awake()
    {
        // 영웅 소환
        heroList = GameManager.Instance.heroSellectManager.heroList;
        var heroPrefab = heroList.heroPrefab;
        var isSellect = heroList.isSellect;
        for (int i = 0; i < heroList.heroPrefab.Length; i++)
        {

        }

        skillManager.isSellectSkill = false;
        skillManager.isActiveSkill = false;
    }
    private void Update()
    {
        if(stageEnd && Input.GetMouseButtonDown(0))
        {
            Debug.Log("메인 메뉴로");

            // 메인 씬으로 이동...
            GameManager.Instance.ChangeScene("Main01");
        }
        /***************************************************
         * 스테이지 테스트
         * ************************************************/
        if (Input.GetKeyDown(KeyCode.V))
        {
            Victory();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Defeat();
        }
        /***************************************************
         * 스킬 관리
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
                    foreach (SpriteRenderer ren in skillAreaRender)
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
        }
    }
    /***************************************************
     * 성공
     * ************************************************/
    public void Victory()
    {
        Instantiate(VictoryUI).transform.SetParent(GameObject.Find("Canvas").transform, false);
        stageEnd = true;

        // 영웅들 애니메이션 변경.... SetTrigger("Clear");
        // GameManager에게 보상 전달
    }
    /***************************************************
     * 실패
     * ************************************************/
    public void Defeat()
    {
        if (--HeroCount == 0)
        {
            Instantiate(DefeatUI).transform.SetParent(GameObject.Find("Canvas").transform, false);
            stageEnd = true;
        }
        Debug.Log("HeroCount : " + HeroCount);
    }
}
