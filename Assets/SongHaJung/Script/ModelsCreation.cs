using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsCreation : MonoBehaviour
{
    private List<GameObject> Models;
    public GameDataManager Datamanager;

    void Start()
    {

        GameDataManager.selectionIndex = 0;
        Models = new List<GameObject>();

        foreach (Transform t in transform)
        {
            Models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        
        Models[GameDataManager.selectionIndex].SetActive(true);

    }
    public void Update()
    {
       
        Select(GameDataManager.selectionIndex);
        Debug.Log(GameDataManager.selectionIndex);

    }
    public void Select(int index)
    {

        //if (index < 0 || index >= Models.Count) 
        //{
        //    return;
        //}

        //if (index == GameDataManager.selectionIndex)
        //{
        //    return;
        //}

        if (index < 2)
        {
            Models[GameDataManager.selectionIndex].SetActive(false);
        }

        
        Models[GameDataManager.selectionIndex].SetActive(true);
        Datamanager.EndBuilding();



    }

}
