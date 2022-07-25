using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject building, Blur, buttons, maps;

    public GameObject backbutton;

    private void Start()
    {
        backbutton.gameObject.SetActive(false);
    }

    //public void SceneChange()
    //{
    //    GameManager.Instance.GoBattleScene();
    //}

    public void OnclickSystem()
    {
        buttons.SetActive(false);
        Blur.SetActive(true);
        backbutton.gameObject.SetActive(false);
    }
    public void OnclickStory()
    {
        
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

        backbutton.gameObject.SetActive(false);
    }
    public void BuildingOnMain()
    {
        buttons.SetActive(false);
        Blur.SetActive(false);
        building.SetActive(false);

        backbutton.gameObject.SetActive(true);
    }

    public void OnClickCookStore(GameObject CookMenu)
    {
        Blur.SetActive(true);
        buttons.SetActive(false);
        CookMenu.SetActive(true);
    }

    public void OffClickCookStore(GameObject CookMenu)
    {
        Blur.SetActive(false);
        buttons.SetActive(true);
        CookMenu.SetActive(false);
    }


    public void OffClickMap()
    {
        maps.SetActive(false);
        Blur.SetActive(false);
        buttons.SetActive(true);
    }
    public void OnClickMap()
    {
        maps.SetActive(true);
        Blur.SetActive(true);
        buttons.SetActive(false);
    }
 
    public void OnClickBuilding()
    {
        //돌아가기 클릭 -> buildingmodels setactive(false)
        OnclickSystem();
        building.SetActive(true);
    }

    //배치다됐을때
    public void OnClickCheckBuilding()
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
