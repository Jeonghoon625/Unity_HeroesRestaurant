using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    private Button button;
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
    }
    public void OnClickMenu()
    {
        button.enabled = false;
        Time.timeScale = 0f;
        menu.SetActive(true);
    }

    public void Continue()
    {
        button.enabled = true;
        Time.timeScale = 1f;
        menu.SetActive(false);
    }

    public void Exit()
    {
        button.enabled = true;
        Time.timeScale = 1f;
        menu.SetActive(false);

        // 바로 게임 실패 모든 유닛들 정지
        var stageManager = GameObject.FindWithTag("GameController").GetComponent<StageManager>();
        foreach(var hero in stageManager.herosList)
        {
            hero.GetComponent<Heros>().SetState("None");
        }

        foreach (var enemy in stageManager.enemyList)
        {
            enemy.GetComponent<Enemy>().SetState("None");
        }

        stageManager.Defeat();
    }
}
