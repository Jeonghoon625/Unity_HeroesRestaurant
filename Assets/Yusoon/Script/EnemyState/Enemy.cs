using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum MonsterType
{
    Normal,
    Boss,
}
public class Enemy : MonoBehaviour
{
    [SerializeField]
    public MonsterType monsterType;
    public BoarBoss boarBoss;

    public Image hpBar;
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
    public float maxHp;

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
        maxHp = hp;
        hpBar.gameObject.SetActive(false);

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
        if(monsterType == MonsterType.Boss)
        {
            boarBoss.BossUpdate();
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
        hpBar.gameObject.SetActive(true);
        col.enabled = true;
        SetState("Idle");
        stageManager.enemyList.Add(gameObject);
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
                //SetState("Idle");
            }
        }
    }
    /******************************************
     * 맞은 판정
     * ***************************************/
    void DeadEffect()
    {
        // 사라질 때 이펙트
        if (monsterType != MonsterType.Boss)
        {
            Destroy(gameObject);
        }
    }
    public float OnHit(Heros attacker, float dmg)
    {
        // hp 감소
        hp -= dmg;
        hpBar.GetComponent<HpBar>().HitHp(hp, maxHp);
        if (hp <= 0)
        {
            foreach (var s in stageManager.herosList)
            {
                s.GetComponent<Heros>().target = null;
            }
            Dead();
            attacker.target = null;
        }
        return hp;
    }
    public void Dead()
    {
        if (OnDeath != null)
            OnDeath();
        if(monsterType == MonsterType.Boss)
        {
            gameObject.GetComponent<BoarBoss>().isAlive = false;
        }
        SetState("None");
        animator.SetTrigger("Dead");
        col.enabled = false;
        hp = 0;

        stageManager.DeadEnemy(gameObject);

    }
}
