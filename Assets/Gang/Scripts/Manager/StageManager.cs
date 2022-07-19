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
    private bool stageEnd = false;
    public int HeroCount;
    public GameObject VictoryUI;
    public GameObject DefeatUI;

    private HeroList heroList;
    private void Awake()
    {
        // ���� ��ȯ
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
            Debug.Log("���� �޴���");

            // ���� ������ �̵�...
            GameManager.Instance.ChangeScene("Main01");
        }
        /***************************************************
         * �������� �׽�Ʈ
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
        Instantiate(VictoryUI).transform.SetParent(GameObject.Find("Canvas").transform, false);
        stageEnd = true;

        // ������ �ִϸ��̼� ����.... SetTrigger("Clear");
        // GameManager���� ���� ����
    }
    /***************************************************
     * ����
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
