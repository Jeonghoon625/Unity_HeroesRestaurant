using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.DoSomething();
        GameManager.Instance.DoSomething();
    }
}
