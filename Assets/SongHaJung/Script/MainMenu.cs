using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject building, Blur, buttons, maps, upgrade;

    public GameObject backbutton,rebuild;

    private void Start()
    {
        backbutton.gameObject.SetActive(false);
    }


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

    public void BuildingOnMain2(GameObject rebuild)
    {
        buttons.SetActive(false);
        Blur.SetActive(false);
        building.SetActive(false);

        rebuild.gameObject.SetActive(true);
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
    
    public void OnClickUpgrade(GameObject Upgrade)
    {
        Blur.SetActive(true);
        buttons.SetActive(false);
        Upgrade.SetActive(true);
    }
    public void OffClickUpgrade(GameObject Upgrade)
    {
        Blur.SetActive(false);
        buttons.SetActive(true);
        Upgrade.SetActive(false);
    }

    public void OnClickInfo(GameObject Info)
    {
        Blur.SetActive(true);
        buttons.SetActive(false);
        Info.SetActive(true);
    }
    public void OffClickInfo(GameObject Info)
    {
        Blur.SetActive(false);
        buttons.SetActive(true);
        Info.SetActive(false);
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

    public void OnClickExit(GameObject ExitMenu)
    {
        Blur.SetActive(true);
        buttons.SetActive(false);
        ExitMenu.SetActive(true);
    }

    public void OffClickExit(GameObject ExitMenu)
    {
        Blur.SetActive(false);
        buttons.SetActive(true);
        ExitMenu.SetActive(false);
    }

    public void OnClickQuit()
    {
        GameManager.Instance.saveLoadManager.Save();
        Application.Quit();
    }

}
