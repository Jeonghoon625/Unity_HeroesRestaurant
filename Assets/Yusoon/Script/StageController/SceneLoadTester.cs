using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoadTester : MonoBehaviour
{
    private bool isTouch = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isTouch)
        {
            isTouch = true;
            StartCoroutine(timePeriod());
        }
    }

    IEnumerator timePeriod()
    {
        float timer = 0;

        while(timer > 1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("LoadingScene");
    }
}
