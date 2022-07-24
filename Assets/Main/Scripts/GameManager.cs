using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isInit;
    public int masterStage = 1;
    public List<int> slaveStage = new List<int>();

    public GameManager()
    {
        isInit = false;
        Debug.Log("게임 매니저 초기화");
        goodsManager = new GoodsManager();
        resourceManager = new ResourceManager();
        saveLoadManager = new SaveLoadManager();
    }

    public void GoBattleScene()
    {
        SceneLoader.LoadScene("Gang");
    }

    public void ChanageScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }

    public void Init()
    {
        isInit = true;
    }

    //Cook
    public ResourceManager resourceManager;

    //Goods
    public GoodsManager goodsManager;

    public SaveLoadManager saveLoadManager;
}