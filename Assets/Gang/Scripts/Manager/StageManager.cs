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
    private bool stageEnd = false;          // 임시
    public int HeroCount;                   // 임시
    public GameObject VictoryUI;
    public GameObject DefeatUI;

    private HeroList heroList;
    public List<GameObject> herosList;
    /******************************************
     * 오브젝트 풀
     * ***************************************/
    public MultipleObjectPooling objectPool;
    private void Start()
    {
        // 영웅 소환
        /*********************************************************** 게임 매니저 수정 후 사용 *******************************************************************/
        //heroList = GameManager.Instance.heroSellectManager.heroList;
        //var heroPrefab = heroList.heroPrefab;
        //var isSellect = heroList.isSellect;
        //var ren = heroList.heroPrefab.Length;
        //int count = 0;
        //for (int i = 0; i < ren; i++)
        //{
        //    if (isSellect[i] == true)
        //    {
        //        count++;
        //        // 오브젝트 풀
        //        var hero = heroPrefab[i].GetComponent<Heros>();
        //        if (hero.AttackType == AttackTypes.Range)
        //        {
        //            objectPool.poolPrefabs.Add(hero.shootPrefab);
        //        }
        //    }
        //}
        //float startPos = -0.75f * (count - 1);
        //var pos = Vector3.zero;
        //var rot = Quaternion.identity;
        //rot.y = 180f;
        //for (int i = 0; i < ren; i++)
        //{
        //    if (isSellect[i] == true)
        //    {
        //        pos.x = startPos;
        //        herosList.Add(Instantiate(heroPrefab[i], pos, rot));
        //        startPos += 1.5f;
        //    }
        //}

        skillManager.isSellectSkill = false;
        skillManager.isActiveSkill = false;
        // 오브젝트 풀
        objectPool.Test();
    }
    private void Update()
    {
        if(stageEnd && Input.GetMouseButtonDown(0))
        {
            Debug.Log("메인 메뉴로");

            // 메인 씬으로 이동...
            //GameManager.Instance.ChangeScene("Main01");
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

        foreach(var heroState in herosList)
        {
            var con = heroState.GetComponent<Heros>();
            con.animator.SetTrigger("Clear");
            con.SetState("None");
        }
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
    }
}
