using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float speed = 3f;
    public float ran = 15f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var cameraPos = Input.mousePosition;
            var MousePostion = Camera.main.ScreenToWorldPoint(new Vector3(cameraPos.x, cameraPos.y, ran));

            var pos = transform.position;
            var rot  = transform.rotation;
            if(pos.x - MousePostion.x > 0f)
            {
                rot.y = 180;
            }
            else
            {
                rot.x = 0;
            }
            Debug.Log(pos.x - MousePostion.x);
            transform.rotation = rot;

            pos.x = MousePostion.x;
            transform.position = pos;
        }
    }
}
