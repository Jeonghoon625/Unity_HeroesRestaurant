using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /******************************************
     * ����
     * ***************************************/
    public Dictionary<string, IEnemyState> stateMap;
    public IEnemyState currentState;
    public string prevState;

    public Animator animator;
    /******************************************
     * ����
     * ***************************************/
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private int dmg = 3;

    public Vector3 attackArea = new Vector3(1f, 0f, 0f);
    public int hp = 100;

    [SerializeField]
    private GameObject skillPrefab;
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

    private void Awake()
    {
        /*******************************************************************************/
        // ü��, ���ݷ� => ������ ���̺� �ε带 ���Ͽ� ����
        /*******************************************************************************/

        /*******************************************************************************/
        // ĳ���� ���� �� ��ų â�� ��ų ��ư �߰�
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
        if (Input.GetMouseButtonDown(0))
        {
            //animator.SetBool("Attack", false);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.transform.tag == "Monster")
                {
                    // Ÿ����
                    target = hit.transform.gameObject;
                }
                else
                {
                    target = null;
                }
                // �̵�
                animator.SetBool("Attack", false);
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

        IEnemyState nextState = stateMap[stateName];
        currentState = nextState;
        currentState.IEnter(this);
        currentState.IUpdate();
    }
}