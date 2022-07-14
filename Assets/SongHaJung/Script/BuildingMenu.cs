using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    public void OnClickBack()
    {
        MenuManager.OpenMenu(Menu.MAINMENU, gameObject);
    }
}
