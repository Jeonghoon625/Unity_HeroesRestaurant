using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorials;
    private int idx = 0;

    public GameObject spawner;

    private void Start()
    {
        Invoke("ShowUI1", 2.5f);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            tutorials[idx].gameObject.SetActive(false);
            idx++;

            ShowUI1();
        }
    }

    public void ShowUI1()
    {
        tutorials[idx].gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
