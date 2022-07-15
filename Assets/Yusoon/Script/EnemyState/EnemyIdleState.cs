using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private Enemy enemy;
    private GameObject leftTarget;
    private GameObject rightTarget;

    private float leftDir;
    private float rightDir;

    public void IEnter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void IUpdate()
    {
        if (enemy.target == null)
        {
            // Raycast 타깃 추적
            var unitPos = enemy.transform.position;
            RaycastHit hit;
            /****************************************************************************************************************************/
            int layerMast = (-1) - (1 << LayerMask.NameToLayer("Monster"));
            if (Physics.Raycast(unitPos + Vector3.up, Vector3.left, out hit, 30f, layerMast))
            {
                /****************************************************************************************************************************/
                if (hit.transform.tag == "Hero")
                {
                    leftDir = unitPos.x - hit.transform.position.x;
                    leftTarget = hit.transform.gameObject;
                    // 일반
                    enemy.target = leftTarget;
                    enemy.m_Position = enemy.target.transform.position;
                }
            }

            if (Physics.Raycast(unitPos + Vector3.up, Vector3.right, out hit, 30f, layerMast))
            {
                /****************************************************************************************************************************/
                if (hit.transform.tag == "Hero")
                {
                    rightDir = unitPos.x - hit.transform.position.x;
                    rightTarget = hit.transform.gameObject;
                    // 일반
                    enemy.target = rightTarget;
                    enemy.m_Position = enemy.target.transform.position;
                }
            }

            if (leftTarget != null && rightTarget != null)
            {
                leftDir = Mathf.Abs(leftDir);
                rightDir = Mathf.Abs(rightDir);
                enemy.target = leftDir < rightDir ? leftTarget : rightTarget;
                enemy.m_Position = enemy.target.transform.position;
            }

            if (enemy.target != null)
            {
                enemy.SetState("Run");
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
