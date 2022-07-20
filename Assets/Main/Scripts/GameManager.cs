using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int test = 625;

    public GameManager()
    {
        Debug.Log("게임 매니저 초기화");
    }

    public void GoBattleScene()
    {
        SceneLoader.LoadScene("Gang");
    }

    public void DoSomething()
    {
        Debug.Log(test);
    }
}