
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    void Awake()
    {
        Debug.Log("���ӸŴ��� ����");

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
        heroSellectManager = this.gameObject.AddComponent<HeroSellectManager>();
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

    //�Ŵ����� ����ٰ� �����ϴ°� �����Ͱ��ƿ�

    public CookManager cookMgr;

    











    public HeroSellectManager heroSellectManager;
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