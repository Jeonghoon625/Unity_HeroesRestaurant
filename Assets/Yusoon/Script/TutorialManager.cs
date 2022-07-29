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
        ShowUI();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            tutorials[idx].gameObject.SetActive(false);
            idx++;
            //ShowUI();
            //tutorials[1].gameObject.SetActive(true);
        }

        //Time.timeScale = 0f;
        //tutorials[2].gameObject.SetActive(true);

        //if(Input.GetMouseButtonDown(0))
        //{
        //    Time.timeScale = 1f;
        //}

        //Time.timeScale = 0f;
        //tutorials[3].gameObject.SetActive(true);

        //if(Input.GetMouseButtonDown(0))
        //{
        //    Time.timeScale = 1f;
        //}

        //tutorials[4].gameObject.SetActive(true);
        //tutorials[5].gameObject.SetActive(true);
    }

    public void ShowUI()
    {
        tutorials[idx].gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
