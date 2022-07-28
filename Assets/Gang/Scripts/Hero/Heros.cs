using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum AttackTypes
{
    Melee,
    Range,
}
public class Heros : MonoBehaviour
{
    public Vector3 bossPos;

    public Reinforcement reinforcement;
    public Image hpBar;
    public StageManager stageManager;
    public GameObject stunPrefab;
    private GameObject stunObj;
    /******************************************
     * ����
     * ***************************************/
    public Dictionary<string, IState> stateMap;
    public IState currentState;


    public string curStateString;
    public string prevStateString;

    public Animator animator;

    private BoxCollider col;
    /******************************************
     * ���� �� ����
     * ***************************************/
    [SerializeField]
    private AttackTypes attackType;                             // ���� Ÿ��
    [SerializeField]
    private float speed = 3f;                                   // �̵��ӵ�
    [SerializeField]
    private int dmg = 3;                                        // ���ݷ�
    [SerializeField]
    private float attackCool = 1f;                              // ���� ��Ÿ��
    [SerializeField]
    private float armor = 0f;                                  // ����
    public float maxShield;                                      // ����
    public float curShield;                                      // ����

    public Vector3 attackArea = new Vector3(1f, 0f, 0f);        // ���� ����
    public float hp = 100;                                        // ü��
    public float maxHp;                                        // ü��

    public GameObject skillButtonPrefab;                        // ��ų ��ư
    public GameObject skillButton;                        // ��ų ��ư
    //public GameObject skillPrefab;                              // ��ų
    [SerializeField]
    public GameObject shootPrefab;                             // ����ü
    public GameObject startShoot;                               // ����ü �߻� ��ġ

    public Vector3 m_Position;                                  // ��ǥ �̵�����
    public bool isMovePoint;
    public GameObject target;                                   // ���� ���
    /******************************************
     * ��ų
     * ***************************************/
    public bool isInvincibility;                                // ����
    public bool isShield;                                       // ����
    public bool doneControll;                                  // ��ų ���� ��

    public float runSpeed
    {
        get { return speed; }
    }
    public int Dmg
    {
        get { return dmg; }
    }
    public float AttackCool
    {
        get { return attackCool; }
    }
    public AttackTypes AttackType
    {
        get { return attackType; }
    }

    private void Awake()
    {
        var enhance = GameManager.Instance.goodsManager.enhance;
        // ��ȭ ����
        dmg += Mathf.RoundToInt(dmg * reinforcement.power / 100f + enhance);
        hp += Mathf.Round(hp * reinforcement.health / 100f + enhance);

        maxHp = hp;
        maxShield = hp * 0.2f;
        curShield = maxShield;
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        /*******************************************************************************/
        // ü��, ���ݷ� => ������ ���̺� �ε带 ���Ͽ� ����
        /*******************************************************************************/
        // ��ų â�� ��ų ��ư �߰�
        skillButton = Instantiate(skillButtonPrefab);
        skillButton.transform.SetParent(GameObject.Find("Skill").transform, false);
        // StageManager�� ���� ����
        stageManager = GameObject.FindWithTag("GameController").GetComponent<StageManager>();
        stageManager.herosList.Add(gameObject);

        /*******************************************************************************/
        // ĳ���� ���� ����
        /*******************************************************************************/
        col = gameObject.GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();

        stateMap = new Dictionary<string, IState>();

        stateMap.Add("Idle", new IdleState());
        stateMap.Add("Run", new RunState());
        stateMap.Add("Attack", new AttackState());
        stateMap.Add("Stun", new StunState());
        stateMap.Add("None", new NoneState());

        SetState("Idle");
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !doneControll)
        //{
        //    target = null;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 100f))
        //    {
        //        if (hit.transform.tag == "Monster")
        //        {
        //            // Ÿ����
        //            target = hit.transform.gameObject;
        //        }
        //        // �̵�
        //        m_Position = hit.point;
        //        SetState("Run");
        //    }
        //}

        currentState.IUpdate();
    }
    private void FixedUpdate()
    {
        currentState.IFixedUpdate();
    }
    /******************************************
     * ���� ����
     * ***************************************/
    public void SetState(string stateName)
    {
        if (currentState != null)
        {
            currentState.IExit();
        }

        curStateString = stateName;
        IState nextState = stateMap[stateName];
        currentState = nextState;
        currentState.IEnter(this);
        currentState.IUpdate();
    }
    /*************************************************************** ���� ���� ���� ������� ***************************************************************/
    /******************************************
     * �ִϸ��̼� �̺�Ʈ ����!!
     * ***************************************/
    void OnAttack()
    {
        if(target == null)
        {
            return;
        }
        var monsterStat = target.GetComponent<Enemy>();
        switch (AttackType)
        {
            case AttackTypes.Range:
                RangeAttack(monsterStat);
                break;
            case AttackTypes.Melee:
                MelleAttack(monsterStat);
                break;
        }
    }
    void NextTarget()
    {
        if(target == null)
        {
            return;
        }
        var monsterStat = target.GetComponent<Enemy>();
        if (monsterStat != null)
        {
            if (monsterStat.OnHit(this, 0) == 0)
            {
                SetState("Idle");
            }
        }
    }
    private void MelleAttack(Enemy monster)
    {
        if (monster != null)
        {
            monster.OnHit(this, Dmg);
        }
    }
    private void RangeAttack(Enemy monster)
    {
        if (monster != null)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        var rot = Quaternion.identity;
        rot.y = transform.rotation.y == 0 ? 180f : 0f;
        Instantiate(shootPrefab).GetComponent<ShootableObject>().Set(this, startShoot.transform.position, rot);
    }
    /******************************************
     * ���� ����
     * ***************************************/
    void DeadEffect()
    {
        // ����� �� ����Ʈ
        Destroy(gameObject);
    }
    public float OnHit(Enemy attacker, int dmg)
    {
        // ���� ����
        if(isInvincibility)
        {
            return hp;
        }
        if(isShield)
        {
            curShield -= (dmg - armor);
            if(hpBar.GetComponent<HpBar>().HitShield(curShield, maxShield) <= 0)
            {
                isShield = false;
                Destroy(gameObject.GetComponentInChildren<HeroSkill>().gameObject);
            }
            
            return hp;
        }
        // hp ����
        hp -= (dmg - armor);
        hpBar.GetComponent<HpBar>().HitHp(hp, maxHp);
        if(hp <= 0)
        {
            foreach (var s in stageManager.enemyList)
            {
                s.GetComponent<Enemy>().target = null;
            }
            Dead(attacker.target);
        }
        return hp;
    }
    private void Dead(GameObject target)
    {
        animator.SetTrigger("Dead");
        SetState("None");
        target = null;
        col.enabled = false;
        hp = 0;
        doneControll = true;
        skillButton.GetComponent<Button>().interactable = false;

        stageManager.Defeat(gameObject);
    }
    /******************************************
     * ��ų
     * ***************************************/
    void SkillEnd()
    {
        SetState(prevStateString);
    }
    /******************************************
     * ����
     * ***************************************/
    public void StartStun()
    {
        var pos = transform.position;
        pos.y += 2.5f;
        //stunObj = (Instantiate(stunPrefab, pos, transform.rotation).transform.parent = transform);
        stunObj = Instantiate(stunPrefab, pos, transform.rotation);
        stunObj.transform.parent = transform;
    }
    public void EndStun()
    {
        Destroy(stunObj);
    }
}
