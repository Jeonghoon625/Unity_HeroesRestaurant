using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Tables\\Food_DataTable");

        for(var i = 0; i < data.Count; i++)
        {
            Debug.Log(data[i]["name"].ToString());
        }

    }
}
