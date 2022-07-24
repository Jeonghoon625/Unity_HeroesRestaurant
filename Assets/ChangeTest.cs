using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTest : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.saveLoadManager.Save();
        SceneManager.LoadScene("Cook");
    }
}
