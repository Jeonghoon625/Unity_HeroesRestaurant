using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSkill : MonoBehaviour
{
    public float areaX = 1.5f;
    private float slowTime = 4f;
    private float timer;
    private Vector3 area = Vector3.zero;

    void Start()
    {
        area.x = areaX;
    }

    void Update()
    {
        var cols = Physics.OverlapBox(transform.position, area);
        timer += Time.deltaTime;
        if(timer > slowTime)
        {
            foreach (var col in cols)
            {
                if (col.gameObject.tag == "Monster")
                {
                    col.GetComponent<Enemy>().speedDebuff = 1;
                }
            }
            
            Destroy(gameObject);
            return;
        }
        foreach(var col in cols)
        {
            if (col.gameObject.tag == "Monster")
            {
                col.GetComponent<Enemy>().speedDebuff = 0;
            }
        }
    }
}
