using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject cookStore, building, Blur, buttons, Story;
    public Button backbutton;

    private void Start()
    {
        backbutton.gameObject.SetActive(false);
        Story.SetActive(false);
        cookStore.SetActive(false);
        
    }

    public void SceneChange()
    {
        GameManager.Instance.GoBattleScene();
    }

    public void OnclickSystem()
    {
        buttons.SetActive(false);
        Blur.SetActive(true);
        backbutton.gameObject.SetActive(false);
    }
    public void OnclickStory()
    {
        Story.SetActive(true);
        Blur.SetActive(true);
        buttons.SetActive(false);
    }
    /*
    public void OnclickCook()
    {
        cookStore.SetActive(true);
        Blur.SetActive(true);
        buttons.SetActive(false);
    }
    */

    public void BackclickSystem()
    {
        buttons.SetActive(true);
        Blur.SetActive(false);

        Story.gameObject.SetActive(false);
        cookStore.SetActive(false);

        backbutton.gameObject.SetActive(false);
    }
    public void BuildingOnMain()
    {
        buttons.SetActive(false);
        Blur.SetActive(false);
        building.SetActive(false);

        backbutton.gameObject.SetActive(true);
       
    }

    public void OnClickCookStore()
    {
        cookStore.SetActive(true);
        Blur.SetActive(true);
        buttons.SetActive(false);
    }
 
    public void OnClickBuilding()
    {
        OnclickSystem();
        building.SetActive(true);
        
    }
    public void OnClickBuildingBack()
    {
        buttons.SetActive(true);
        Blur.SetActive(false);

        backbutton.gameObject.SetActive(false);

        building.SetActive(false);
        
    }

}