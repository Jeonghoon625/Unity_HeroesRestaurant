using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isCookInit;
    public int masterStage = 1;
    public List<int> slaveStage = new List<int>();

    public GameManager()
    {
        isCookInit = false;
        Debug.Log("게임 매니저 초기화");
        goodsManager = new GoodsManager();
        resourceManager = new ResourceManager();
        saveLoadManager = new SaveLoadManager();
        awardManager = new AwardManager();
        soundManager = new SoundManager();
    }

    public void GoBattleScene()
    {
        SceneLoader.LoadScene("Gang");
    }

    public void ChanageScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }

    public void CookInit()
    {
        isCookInit = true;
    }

    //Cook
    public ResourceManager resourceManager;

    //Goods
    public GoodsManager goodsManager;

    public SaveLoadManager saveLoadManager;

    public AwardManager awardManager;

    public SoundManager soundManager;
}