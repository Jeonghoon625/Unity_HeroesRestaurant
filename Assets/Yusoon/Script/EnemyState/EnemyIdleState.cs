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
            // Raycast 鸥标 眠利
            var enemyPos = enemy.transform.position;
            
            RaycastHit hit;

            int layerMast = (-1) - (1 << LayerMask.NameToLayer("Monster"));
            if (Physics.Raycast(enemyPos + Vector3.up, Vector3.left, out hit, 30f, layerMast))
            {
                if (hit.transform.tag == "Hero")
                {
                    leftDir = enemyPos.x - hit.transform.position.x;
                    leftTarget = hit.transform.gameObject;
                    // 老馆
                    enemy.target = leftTarget;
                }
            }

            if (Physics.Raycast(enemyPos + Vector3.up, Vector3.right, out hit, 30f, layerMast))
            {
                if (hit.transform.tag == "Hero")
                {
                    rightDir = enemyPos.x - hit.transform.position.x;
                    rightTarget = hit.transform.gameObject;
                    // 老馆
                    enemy.target = rightTarget;
                }
            }

            if (leftTarget != null && rightTarget != null)
            {
                leftDir = Mathf.Abs(leftDir);
                rightDir = Mathf.Abs(rightDir);
                enemy.target = leftDir < rightDir ? leftTarget : rightTarget;
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
