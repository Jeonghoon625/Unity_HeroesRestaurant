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
    [SerializeField]
    private AttackTypes attackType;
    /******************************************
     * ����
     * ***************************************/
    public Dictionary<string, IState> stateMap;
    public IState currentState;
    public string prevState;

    public Animator animator;
    /******************************************
     * ����
     * ***************************************/
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private int dmg = 3;
    [SerializeField]
    private float attackCool = 1f;

    public Vector3 attackArea = new Vector3(1f, 0f, 0f);
    public int hp = 100;

    [SerializeField]
    private GameObject skillPrefab;
    [SerializeField]
    private GameObject shootPrefab;
    public GameObject startShoot;
    /******************************************
     * �̵� �� ��
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

        animator = GetComponent<Animator>();

        stateMap = new Dictionary<string, IState>();

        stateMap.Add("Idle", new IdleState());
        stateMap.Add("Run", new RunState());
        stateMap.Add("Attack", new AttackState());
        stateMap.Add("Stun", new StunState());

        SetState("Idle");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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

        IState nextState = stateMap[stateName];
        currentState = nextState;
        currentState.IEnter(this);
        currentState.IUpdate();
    }
    /******************************************
     * �ִϸ��̼� �̺�Ʈ ����!!
     * ***************************************/
    void PerformNextSkillAction()
    {
        if(target == null)
        {
            return;
        }
        var monster = target.GetComponent<MonsterState>();
        var dir = transform.position.x - target.transform.position.x;
        switch (AttackType)
        {
            case AttackTypes.Range:
                RangeAttack(monster, dir);
                break;
            case AttackTypes.Melee:
                MelleAttack(monster, dir);
                break;
        }
    }
    private void MelleAttack(MonsterState monster, float dir)
    {
        if (monster != null)
        {
            if (monster.OnHit(this, Dmg) == 0)
            {
                SetState("Idle");
            }
        }
    }
    private void RangeAttack(MonsterState monster, float dir)
    {
        if (monster != null)
        {
            Shoot(dir);
        }
    }
    private void Shoot(float dir)
    {
        var rot = Quaternion.identity;
        rot.y = transform.rotation.y == 0 ? 180f : 0f;
        Instantiate(shootPrefab, startShoot.transform.position, rot);
    }
}
