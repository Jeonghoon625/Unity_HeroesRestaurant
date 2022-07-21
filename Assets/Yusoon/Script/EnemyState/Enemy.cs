using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public StageManager stageManager;
    /******************************************
     * 상태
     * ***************************************/
    public Dictionary<string, IEnemyState> stateMap;
    public IEnemyState currentState;
    public string prevState;

    public Animator animator;

    private BoxCollider col;
    /******************************************
     * 스탯
     * ***************************************/
    [SerializeField]
    private float speed = 3f;
    public int speedDebuff = 1;
    [SerializeField]
    private int dmg = 3;

    public float attackAreaX;
    public Vector3 attackArea;
    public float hp = 100;

    [SerializeField]
    private float attackCool = 0.5f;
    /******************************************
     * 이동 및 적
     * ***************************************/
    public GameObject target;

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

    /******************************************
    * 이벤트
    * ***************************************/
    public event System.Action OnDeath;

    private void Awake()
    {
        stageManager = GameObject.FindWithTag("GameController").GetComponent<StageManager>();
        /*******************************************************************************/
        // 체력, 공격력 => 데이터 세이브 로드를 통하여 관리
        /*******************************************************************************/

        col = gameObject.GetComponent<BoxCollider>();
        col.enabled = false;

        animator = GetComponent<Animator>();

        stateMap = new Dictionary<string, IEnemyState>();

        stateMap.Add("None", new EnumyNoneState());
        stateMap.Add("Idle", new EnemyIdleState());
        stateMap.Add("Run", new EnemyRunState());
        stateMap.Add("Attack", new EnemyAttackState());

        SetState("None");
    }
    private void Start()
    {
        attackArea = new Vector3(attackAreaX, 0f, 0f);
    }
    private void Update()
    {
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

        IEnemyState nextState = stateMap[stateName];
        currentState = nextState;
        currentState.IEnter(this);
        currentState.IUpdate();
    }
    /******************************************
     * 애니메이션 이벤트 생성
     * ***************************************/
    void Create()
    {
        col.enabled = true;
        SetState("Idle");
    }

    /******************************************
     * 애니메이션 이벤트 공격!!
     * ***************************************/
    void OnAttack()
    {
        if (target == null)
        {
            return;
        }
        var hero = target.GetComponent<Heros>();
        MelleAttack(hero);
    }
    private void MelleAttack(Heros hero)
    {
        if (hero != null)
        {
            if (hero.OnHit(this, Dmg) == 0)
            {
                SetState("Idle");
            }
        }
    }
    /******************************************
     * 맞은 판정
     * ***************************************/
    void DeadEffect()
    {
        // 사라질 때 이펙트
        Destroy(gameObject);
    }
    public float OnHit(Heros attacker, float dmg)
    {
        // hp 감소
        hp -= dmg;
        if (hp <= 0)
        {
            foreach(var s in stageManager.herosList)
            {
                s.GetComponent<Heros>().target = null;
            }
            Dead(attacker);
        }
        return hp;
    }
    private void Dead(Heros target)
    {
        SetState("None");
        animator.SetTrigger("Dead");
        target.target = null;
        col.enabled = false;
        hp = 0;

        stageManager.DeadEnemy(gameObject);

        if (OnDeath != null)
            OnDeath();
    }
}
