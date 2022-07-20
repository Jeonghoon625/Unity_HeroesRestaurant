using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMapManager : MonoBehaviour
{
    public List<Image> stageMaps = new List<Image>();

    public void OnEnable()
    {
        //Debug.Log("Active");
        
        for(int i = 0; i < GameManager.Instance.masterStage; i++)
        {
            stageMaps[i].enabled = true;
        }
    }
    public void Update()
    {    
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ++GameManager.Instance.masterStage;
            Debug.Log(GameManager.Instance.masterStage);
        }
    }
}
