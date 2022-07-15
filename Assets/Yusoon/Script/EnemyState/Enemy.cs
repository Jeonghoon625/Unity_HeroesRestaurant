using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /******************************************
     * 상태
     * ***************************************/
    public Dictionary<string, IEnemyState> stateMap;
    public IEnemyState currentState;
    public string prevState;

    public Animator animator;
    /******************************************
     * 스탯
     * ***************************************/
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private int dmg = 3;

    public Vector3 attackArea = new Vector3(1f, 0f, 0f);
    public int hp = 100;

    [SerializeField]
    private float attackCool = 0.5f;
    /******************************************
     * 이동 및 적
     * ***************************************/
    public Vector3 m_Position;
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
     

    private void Awake()
    {
        /*******************************************************************************/
        // 체력, 공격력 => 데이터 세이브 로드를 통하여 관리
        /*******************************************************************************/

        animator = GetComponent<Animator>();

        stateMap = new Dictionary<string, IEnemyState>();

        stateMap.Add("Idle", new EnemyIdleState());
        stateMap.Add("Run", new EnemyRunState());
        stateMap.Add("Attack", new EnemyAttackState());

        SetState("Idle");
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
     * 애니메이션 이벤트 공격!!
     * ***************************************/
    //void PerformNextSkillAction()
    //{
    //    if (target == null)
    //    {
    //        return;
    //    }
    //    var monster = target.GetComponent<MonsterState>();
    //    var dir = transform.position.x - target.transform.position.x;
    //    switch (AttackType)
    //    {
    //        case AttackTypes.Range:
    //            RangeAttack(monster, dir);
    //            break;
    //        case AttackTypes.Melee:
    //            MelleAttack(monster, dir);
    //            break;
    //    }
    //}
    //private void MelleAttack(MonsterState monster, float dir)
    //{
    //    if (monster != null)
    //    {
    //        if (monster.OnHit(this, Dmg) == 0)
    //        {
    //            SetState("Idle");
    //        }
    //    }
    //}
    //private void RangeAttack(MonsterState monster, float dir)
    //{
    //    if (monster != null)
    //    {
    //        Shoot(dir);
    //    }
    //}
    //private void Shoot(float dir)
    //{
    //    var rot = Quaternion.identity;
    //    rot.y = transform.rotation.y == 0 ? 180f : 0f;
    //    Instantiate(shootPrefab, startShoot.transform.position, rot);
    //}
}
