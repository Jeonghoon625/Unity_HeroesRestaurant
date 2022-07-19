using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    void Awake()
    {
        Debug.Log("게임매니저 생성");

        if (null == instance)
        {
            instance = this;
            
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        cookMgr = this.gameObject.AddComponent<CookManager>();
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void GoBattleScene()
    {
        SceneLoader.LoadScene("Gang");
    }

    public void ChangeScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }

    //매니저들 여기다가 선언하는게 좋을것같아요

    public CookManager cookMgr;

    











    /*
    public void InitGame()
    {

    }

    public void PauseGame()
    {

    }

    public void ContinueGame()
    {

    }

    public void RestartGame()
    {

    }

    public void StopGame()
    {

    }
    */
}