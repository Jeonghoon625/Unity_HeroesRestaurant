using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public bool stageEnd = false;          //
    public GameObject VictoryUI;
    public GameObject DefeatUI;

    private HeroList heroList;
    public List<GameObject> herosList;
    public List<GameObject> enemyList;
    /******************************************
     * 오브젝트 풀
     * ***************************************/
    //public MultipleObjectPooling objectPool;

    [SerializeField]
    private int stageNumber;
    [SerializeField]
    private int clearReward;
    /******************************************
     * 깃발을세워요~
     * ***************************************/
    [SerializeField]
    private GameObject flagPrefab;
    private GameObject flag;
    private Vector3 flagPos;

    private void Start()
    {
        flag = Instantiate(flagPrefab);
        flagPos = flag.transform.position;
        flag.SetActive(false);
        // 영웅 소환
        //heroList = GameManager.Instance.heroSellectManager.heroList;
        heroList = GameObject.FindWithTag("HeroSellect").GetComponent<HeroSellectManager>().heroList;
        var heroPrefab = heroList.heroPrefab;
        var isSellect = heroList.isSellect;
        var ren = heroList.heroPrefab.Length;
        int count = 0;
        for (int i = 0; i < ren; i++)
        {
            if (isSellect[i] == true)
            {
                count++;
                // 오브젝트 풀
                //var hero = heroPrefab[i].GetComponent<Heros>();
                //if (hero.AttackType == AttackTypes.Range)
                //{
                //    objectPool.poolPrefabs.Add(hero.shootPrefab);
                //}
            }
        }
        float startPos = -0.75f * (count - 1);
        var pos = Vector3.zero;
        pos.z += 0.1f;
        var rot = Quaternion.identity;
        rot.y = 180f;
        for (int i = 0; i < ren; i++)
        {
            if (isSellect[i] == true)
            {
                pos.x = startPos;
                Instantiate(heroPrefab[i], pos, rot);
                //herosList.Add(Instantiate(heroPrefab[i], pos, rot));
                startPos += 1.5f;
            }
        }

        skillManager.isSellectSkill = false;
        skillManager.isActiveSkill = false;
        // 오브젝트 풀
        //objectPool.Test();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (stageEnd)
            {
                GameManager.Instance.ChanageScene("Main01");
                return;
            }

#if UNITY_EDITOR_WIN
            Debug.Log("에디터 빌드");
            if(!EventSystem.current.IsPointerOverGameObject())
#endif
#if UNITY_ANDROID_API
            Debug.Log("안드로이드 빌드");
            Touch touch = Input.touches[0];
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
#endif
            {
                //flag.SetActive(false);
                foreach (var hero in herosList)
                {
                    var heroScript = hero.GetComponent<Heros>();
                    if (!heroScript.doneControll)
                    {
                        heroScript.target = null;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, 100f))
                        {
                            // 이동
                            heroScript.isMovePoint = true;
                            flagPos.x = hit.point.x;
                            heroScript.m_Position = flagPos;
                            // 이때!! 깃발 설치
                            flagPos.x = hit.point.x;
                            flag.SetActive(true);
                            flag.transform.position = flagPos;
                            if (hit.transform.tag == "Monster")
                            {
                                // 타겟팅
                                heroScript.target = hit.transform.gameObject;
                                flag.SetActive(false);
                            }
                            heroScript.SetState("Run");
                        }
                    }
                }
            }
        }

        //if (stageEnd && Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("Next Scene");
        //    // 메인 씬으로 이동...
        //    GameManager.Instance.ChanageScene("Main01");
        //}
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
                    Time.timeScale = 1f;
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
    /***************************************************
     * 성공
     * ************************************************/
    public void Victory()
    {

        var reward = Instantiate(VictoryUI);
        reward.transform.SetParent(GameObject.Find("Canvas").transform, false);
        stageEnd = true;

        foreach (var heroState in herosList)
        {
            var con = heroState.GetComponent<Heros>();
            con.animator.SetTrigger("Clear");
            con.SetState("None");
        }

        // GameManager에게 보상 전달
        var gameManager = GameManager.Instance;
        if (stageNumber == 1)
        {
            if (clearReward == 120)
            {
                gameManager.masterStage++;
            }
        }
        else
        {
            gameManager.masterStage++;
        }
        reward.GetComponent<Reward>().ClearReward(stageNumber, clearReward);
        gameManager.awardManager.Award(stageNumber - 1, clearReward);
        gameManager.goodsManager.wood += 10;
    }
    /***************************************************
     * 실패 영웅 사망
     * ************************************************/
    public void Defeat(GameObject hero)
    {
        herosList.Remove(hero);

        if (herosList.Count == 0)
        {
            Defeat();
        }
    }

    public void Defeat()
    {
        Instantiate(DefeatUI).transform.SetParent(GameObject.Find("Canvas").transform, false);
        stageEnd = true;
    }
    /***************************************************
     * 몬스터 사망
     * ************************************************/
    public void DeadEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    /***************************************************
     * 깃발 제거
     * ************************************************/
    public void FlagDel()
    {
        foreach (var hero in herosList)
        {
            var heroScript = hero.GetComponent<Heros>();
            if (heroScript.isMovePoint)
            {
                return;
            }
        }

        flag.SetActive(false);
    }
}
