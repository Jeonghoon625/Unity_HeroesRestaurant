using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnClickCookStore()
    {

    }

    public void OnClickEnhance()
    {

    }

    public void OnClickBuilding()
    {
        MenuManager.OpenMenu(Menu.BUILDING, gameObject);
    }

}
