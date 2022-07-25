using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public string nextScene;

    [SerializeField]
    Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (!op.isDone)
        {
            if (op.progress < 0.9f || timer < 1f)
            {
                timer += Time.deltaTime;
                progressBar.fillAmount = Mathf.Lerp(0f, 0.9f, timer);
                yield return null;
            }
            else if (op.progress >= 0.9f)
            {
                timer += Time.deltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                yield return new WaitForSeconds(1f);
                op.allowSceneActivation = true;
            }
        }
    }
}

