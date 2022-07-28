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
     * 상태
     * ***************************************/
    public Dictionary<string, IState> stateMap;
    public IState currentState;


    public string curStateString;
    public string prevStateString;

    public Animator animator;

    private BoxCollider col;
    /******************************************
     * 스탯 및 동작
     * ***************************************/
    [SerializeField]
    private AttackTypes attackType;                             // 공격 타입
    [SerializeField]
    private float speed = 3f;                                   // 이동속도
    [SerializeField]
    private int dmg = 3;                                        // 공격력
    [SerializeField]
    private float attackCool = 1f;                              // 공격 쿨타임
    [SerializeField]
    private float armor = 0f;                                  // 방어력
    public float maxShield;                                      // 쉴드
    public float curShield;                                      // 쉴드

    public Vector3 attackArea = new Vector3(1f, 0f, 0f);        // 공격 범위
    public float hp = 100;                                        // 체력
    public float maxHp;                                        // 체력

    public GameObject skillButtonPrefab;                        // 스킬 버튼
    public GameObject skillButton;                        // 스킬 버튼
    //public GameObject skillPrefab;                              // 스킬
    [SerializeField]
    public GameObject shootPrefab;                             // 투사체
    public GameObject startShoot;                               // 투사체 발사 위치

    public Vector3 m_Position;                                  // 목표 이동지점
    public bool isMovePoint;
    public GameObject target;                                   // 공격 대상
    /******************************************
     * 스킬
     * ***************************************/
    public bool isInvincibility;                                // 무적
    public bool isShield;                                       // 쉴드
    public bool doneControll;                                  // 스킬 시전 중

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
        // 강화 적용
        dmg += Mathf.RoundToInt(dmg * reinforcement.power / 100f + enhance);
        hp += Mathf.Round(hp * reinforcement.health / 100f + enhance);

        maxHp = hp;
        maxShield = hp * 0.2f;
        curShield = maxShield;
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        /*******************************************************************************/
        // 체력, 공격력 => 데이터 세이브 로드를 통하여 관리
        /*******************************************************************************/
        // 스킬 창에 스킬 버튼 추가
        skillButton = Instantiate(skillButtonPrefab);
        skillButton.transform.SetParent(GameObject.Find("Skill").transform, false);
        // StageManager에 정보 전달
        stageManager = GameObject.FindWithTag("GameController").GetComponent<StageManager>();
        stageManager.herosList.Add(gameObject);

        /*******************************************************************************/
        // 캐릭터 상태 설정
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
        //            // 타겟팅
        //            target = hit.transform.gameObject;
        //        }
        //        // 이동
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
     * 상태 변경
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
    /*************************************************************** 공격 판정 수정 해줘야함 ***************************************************************/
    /******************************************
     * 애니메이션 이벤트 공격!!
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
     * 맞은 판정
     * ***************************************/
    void DeadEffect()
    {
        // 사라질 때 이펙트
        Destroy(gameObject);
    }
    public float OnHit(Enemy attacker, int dmg)
    {
        // 무적 상태
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
        // hp 감소
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
     * 스킬
     * ***************************************/
    void SkillEnd()
    {
        SetState(prevStateString);
    }
    /******************************************
     * 스턴
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
