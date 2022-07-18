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

    public Vector3 attackArea = new Vector3(1f, 0f, 0f);        // ���� ����
    public int hp = 100;                                        // ü��

    public GameObject skillButtonPrefab;                        // ��ų ��ư
    //public GameObject skillPrefab;                              // ��ų
    [SerializeField]
    private GameObject shootPrefab;                             // ����ü
    public GameObject startShoot;                               // ����ü �߻� ��ġ

    public Vector3 m_Position;                                  // ��ǥ �̵�����
    public GameObject target;                                   // ���� ���
    /******************************************
     * ����
     * ***************************************/
    public bool isInvincibility;                                // ����
    public bool isActiveSkill;                                  // ��ų ���� ��

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
        // ü��, ���ݷ� => ������ ���̺� �ε带 ���Ͽ� ����
        /*******************************************************************************/

        /*******************************************************************************/
        // ĳ���� ���� �� ��ų â�� ��ų ��ư �߰�
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
                    // Ÿ����
                    target = hit.transform.gameObject;
                }
                // �̵�
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
     * ���� ����
     * ***************************************/
    void DeadEffect()
    {
        // ����� �� ����Ʈ
        Destroy(gameObject);
    }
    public int OnHit(Enemy attacker, int dmg)
    {
        // ���� ����
        if(isInvincibility)
        {
            return hp;
        }
        // hp ����
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
     * ��ų
     * ***************************************/
    void SkillEnd()
    {
        SetState(prevStateString);
    }
}
