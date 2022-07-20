using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int masterStage = 1;
    public List<int> slaveStage = new List<int>();

    public GameManager()
    {
        Debug.Log("게임 매니저 초기화");
        goodsManager = new GoodsManager();
        resourceManager = new ResourceManager();
    }

    public void GoBattleScene()
    {
        SceneLoader.LoadScene("Gang");
    }

    public void DoSomething()
    {
    }

    //Cook
    public ResourceManager resourceManager;

    //Goods
    public GoodsManager goodsManager;

}