using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject cookStore, enhance, heroes, building, Blur, buttons;
    

    private void Start()
    {
       
    }

    public void OnclickSystem()
    {
        buttons.SetActive(false);
        Blur.SetActive(true);
    }

    public void BackclickSystem()
    {
        buttons.SetActive(true);
        Blur.SetActive(false);
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
