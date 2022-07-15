using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyLoadManager : MonoBehaviour
{
    List<Dictionary<string, object>> currencyData = new List<Dictionary<string, object>>();
    public GameObject currencySection;
    public GameObject currencyPrefab;

    private void Awake()
    {
        currencyData = CSVReader.Read("Tables\\Currency_DataTable");
        
        for (var i = 0; i < currencyData.Count; i++)
        {
            Debug.Log(currencyData[i].ToString());
            int id = (int)currencyData[i]["id"];
            string title = (string)currencyData[i]["name"];
            Sprite sprite = Resources.Load<Sprite>("Currency\\" + (string)currencyData[i]["image"]);

            GameObject currencyGO = Instantiate(currencyPrefab);
            currencyGO.transform.SetParent(currencySection.transform);

            CurrencySlot slot = currencyGO.GetComponent<CurrencySlot>();

            slot.id = id;
            slot.title = title;
            slot.sprite = sprite;
        }
    }

}
