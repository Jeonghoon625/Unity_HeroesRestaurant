using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private Heros hero;
    private GameObject leftTarget;
    private GameObject rightTarget;

    private float leftDir;
    private float rightDir;

    public void IEnter(Heros hero)
    {
        this.hero = hero;
    }

    public void IUpdate()
    {
        if(hero.target == null)
        {
            // Raycast 鸥标 眠利
            var unitPos = hero.transform.position;
            RaycastHit hit;
            int layerMast = (-1) - (1 << LayerMask.NameToLayer("Player"));
            if(Physics.Raycast(unitPos + Vector3.up, Vector3.left, out hit, 30f, layerMast))
            {
                if(hit.transform.tag == "Monster")
                {
                    leftDir = unitPos.x - hit.transform.position.x;
                    leftTarget = hit.transform.gameObject;
                    // 老馆
                    hero.target = leftTarget;
                    hero.m_Position = hero.target.transform.position;
                }
            }

            if (Physics.Raycast(unitPos + Vector3.up, Vector3.right, out hit, 30f, layerMast))
            {
                if (hit.transform.tag == "Monster")
                {
                    rightDir = unitPos.x - hit.transform.position.x;
                    rightTarget = hit.transform.gameObject;
                    // 老馆
                    hero.target = rightTarget;
                    hero.m_Position = hero.target.transform.position;
                }
            }

            if(leftTarget != null && rightTarget != null)
            {
                leftDir = Mathf.Abs(leftDir);
                rightDir = Mathf.Abs(rightDir);
                hero.target = leftDir < rightDir ? leftTarget : rightTarget;
                hero.m_Position = hero.target.transform.position;
            }

            if(hero.target != null)
            {
                hero.SetState("Run");
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
