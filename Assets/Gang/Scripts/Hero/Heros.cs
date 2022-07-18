using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AttackTypes
{
    Melee,
    Range,
}
public class Heros : MonoBehaviour
{
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

    public Vector3 attackArea = new Vector3(1f, 0f, 0f);        // 공격 범위
    public int hp = 100;                                        // 체력

    public GameObject skillButtonPrefab;                        // 스킬 버튼
    //public GameObject skillPrefab;                              // 스킬
    [SerializeField]
    private GameObject shootPrefab;                             // 투사체
    public GameObject startShoot;                               // 투사체 발사 위치

    public Vector3 m_Position;                                  // 목표 이동지점
    public GameObject target;                                   // 공격 대상
    /******************************************
     * 버프
     * ***************************************/
    public bool isInvincibility;                                // 무적
    public bool isActiveSkill;                                  // 스킬 시전 중

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
        /*******************************************************************************/
        // 체력, 공격력 => 데이터 세이브 로드를 통하여 관리
        /*******************************************************************************/

        /*******************************************************************************/
        // 캐릭터 생성 시 스킬 창에 스킬 버튼 추가
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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !isActiveSkill)
        {
            target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.transform.tag == "Monster")
                {
                    // 타겟팅
                    target = hit.transform.gameObject;
                }
                // 이동
                m_Position = hit.point;
                SetState("Run");
            }
        }

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
    private void MelleAttack(Enemy monster)
    {
        if (monster != null)
        {
            if (monster.OnHit(this, Dmg) == 0)
            {
                SetState("Idle");
            }
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
        Instantiate(shootPrefab, startShoot.transform.position, rot);
    }
    /******************************************
     * 맞은 판정
     * ***************************************/
    void DeadEffect()
    {
        // 사라질 때 이펙트
        Destroy(gameObject);
    }
    public int OnHit(Enemy attacker, int dmg)
    {
        // 무적 상태
        if(isInvincibility)
        {
            return hp;
        }
        // hp 감소
        hp -= dmg;

        if(hp <= 0)
        {
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
    }
    /******************************************
     * 스킬
     * ***************************************/
    void SkillEnd()
    {
        SetState(prevStateString);
    }
}
