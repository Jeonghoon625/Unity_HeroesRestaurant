using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject cookStore, enhance, heroes, building, Blur, buttons;
    public Button backbutton;

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
    public void OnClickCookStore()
    {
        cookStore.SetActive(true);
    }

    public void OnClickEnhance()
    {
        enhance.SetActive(true);
    }
    public void OnClickHeroes()
    {
        heroes.SetActive(true);
    }
 
    public void OnClickBuilding()
    {
        OnclickSystem();
        building.SetActive(true);
        
    }
    public void OnClickBuildingBack()
    {
        BackclickSystem();
        building.SetActive(false);
        
    }

}
