
using UnityEngine;

public static class MenuManager
{
    public static bool IsInitialised { get; private set; }
    public static GameObject mainMenu, cookStore, enhance, building;
    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        cookStore = canvas.transform.Find("CookStore").gameObject;
        enhance = canvas.transform.Find("Enhance").gameObject;
        building = canvas.transform.Find("BuildingMenu").gameObject;

        IsInitialised = true;
    }

    public static void OpenMenu(Menu menu, GameObject menuName)
    {
        if (!IsInitialised)
            Init();

        switch(menu)
        {
            case Menu.MAINMENU:
                mainMenu.SetActive(true);
                break;
            case Menu.COOKSTORE:
                cookStore.SetActive(true);
                break;
            case Menu.ENHANCE:
                enhance.SetActive(true);
                break;
            case Menu.BUILDING:
                building.SetActive(true);
                break;
        }

        menuName.SetActive(false);
    }
}
