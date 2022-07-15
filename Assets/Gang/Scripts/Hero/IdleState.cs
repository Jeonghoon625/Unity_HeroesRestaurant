using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private Unit unit;
    private GameObject leftTarget;
    private GameObject rightTarget;

    private float leftDir;
    private float rightDir;

    public void IEnter(Unit unit)
    {
        this.unit = unit;
    }

    public void IUpdate()
    {
        if(unit.target == null)
        {
            var unitPos = unit.transform.position;
            RaycastHit hit;
            int layerMast = (-1) - (1 << LayerMask.NameToLayer("Player"));
            if(Physics.Raycast(unitPos + Vector3.up, Vector3.left, out hit, 30f, layerMast))
            {
                if(hit.transform.tag == "Monster")
                {
                    leftDir = unitPos.x - hit.transform.position.x;
                    leftTarget = hit.transform.gameObject;
                    // 일반
                    unit.target = leftTarget;
                    unit.m_Position = unit.target.transform.position;
                }
            }

            if (Physics.Raycast(unitPos + Vector3.up, Vector3.right, out hit, 30f, layerMast))
            {
                if (hit.transform.tag == "Monster")
                {
                    rightDir = unitPos.x - hit.transform.position.x;
                    rightTarget = hit.transform.gameObject;
                    // 일반
                    unit.target = rightTarget;
                    unit.m_Position = unit.target.transform.position;
                }
            }

            if(leftTarget != null && rightTarget != null)
            {
                leftDir = Mathf.Abs(leftDir);
                rightDir = Mathf.Abs(rightDir);
                unit.target = leftDir < rightDir ? leftTarget : rightTarget;
                unit.m_Position = unit.target.transform.position;
            }

            if(unit.target != null)
            {
                unit.SetState("Run");
            }
        }
    }

    public void IFixedUpdate()
    {
        
    }

    public void IExit()
    {
        leftTarget = null;
        rightTarget = null;
    }
}
