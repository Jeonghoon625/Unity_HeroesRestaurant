using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    /******************************************
     * ��ų ����
     * ***************************************/
    public SkillManager skillManager;
    private GameObject skillArea;
    /******************************************
     * �������� ����, ���� ���� Ȯ��
     * ***************************************/
    public bool stageEnd = false;          //
    public GameObject VictoryUI;
    public GameObject DefeatUI;

    private HeroList heroList;
    public List<GameObject> herosList;
    public List<GameObject> enemyList;
    /******************************************
     * ������Ʈ Ǯ
     * ***************************************/
    //public MultipleObjectPooling objectPool;

    [SerializeField]
    private int stageNumber;
    [SerializeField]
    private int clearReward;

    private void Start()
    {
        // ���� ��ȯ
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
                // ������Ʈ Ǯ
                //var hero = heroPrefab[i].GetComponent<Heros>();
                //if (hero.AttackType == AttackTypes.Range)
                //{
                //    objectPool.poolPrefabs.Add(hero.shootPrefab);
                //}
            }
        }
        float startPos = -0.75f * (count - 1);
        var pos = Vector3.zero;
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
        // ������Ʈ Ǯ
        //objectPool.Test();
    }
    private void Update()
    {
        if (stageEnd && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Next Scene");
            // ���� ������ �̵�...
            GameManager.Instance.ChanageScene("Main01");
        }
        /***************************************************
         * ��ų ����
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
        /***************************************************
         * ��ų ���� ������
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
     * ����
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

        // GameManager���� ���� ����
        var gameManager = GameManager.Instance;
        if(stageNumber == 1)
        {
            if(clearReward == 120)
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
     * ���� ���� ���
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
     * ���� ���
     * ************************************************/
    public void DeadEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }
}
