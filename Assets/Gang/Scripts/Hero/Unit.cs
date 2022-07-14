using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /******************************************
     * 상태
     * ***************************************/
    public Dictionary<string, IState> stateMap;
    public IState currentState;
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
    private GameObject skillPrefab;
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

    private void Awake()
    {
        /*******************************************************************************/
        // 체력, 공격력 => 데이터 세이브 로드를 통하여 관리
        /*******************************************************************************/

        /*******************************************************************************/
        // 캐릭터 생성 시 스킬 창에 스킬 버튼 추가
        /*******************************************************************************/

        animator = GetComponent<Animator>();

        stateMap = new Dictionary<string, IState>();

        stateMap.Add("Idle", new IdleState());
        stateMap.Add("Run", new RunState());
        stateMap.Add("Attack", new AttackState());

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
                    // 타겟팅
                    target = hit.transform.gameObject;
                }
                else
                {
                    target = null;
                }
                // 이동
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
     * 상태 변경
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
}
