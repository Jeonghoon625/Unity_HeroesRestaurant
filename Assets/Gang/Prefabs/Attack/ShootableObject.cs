using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour
{
    private float speed = 7f;

    private Vector3 dir = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        dir.x = -1;
    }

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
